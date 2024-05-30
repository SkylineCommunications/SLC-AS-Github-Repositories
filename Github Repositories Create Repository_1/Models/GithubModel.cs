// Ignore Spelling: Github repo

namespace Skyline.DataMiner.Github.Repositories.Models
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Runtime.CompilerServices;
	using System.Text.RegularExpressions;

	using Newtonsoft.Json;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories.Data;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Github.Repositories.Helpers;

	public class GithubModel
	{
		private readonly GithubRepositories interApp;
		private readonly IDmsElement element;

		public GithubModel(IEngine engine, int agentId, int elementId)
		{
			interApp = new GithubRepositories(engine.GetUserConnection(), agentId, elementId);
			element = engine.GetDms().GetElement(new DmsElementId(agentId, elementId));
		}

		public event EventHandler<StatusProgressEventArgs> CreationProgress;

		public List<Team> GetTeamsByOrganization(string organizationId)
		{
			var table = element.GetTable(3400);
			var rows = table.QueryData(new[]
			{
				new ColumnFilter
				{
					Pid = 3401,
				},
				new ColumnFilter
				{
					ComparisonOperator = ComparisonOperator.Equal,
					Pid = 3403,
					Value = organizationId,
				},
				new ColumnFilter
				{
					Pid = 3404,
				},
			});

			return rows.Select(row => new Team
			{
				Id = Convert.ToString(row[0]),
				Organization = Convert.ToString(row[2]),
				Name = Convert.ToString(row[4]),
			}).ToList();
		}

		public List<User> GetUsersByOrganization(string organizationId)
		{
			var linkerTable = element.GetTable(22000);
			var linkerRows = linkerTable.QueryData(new[]
			{
				new ColumnFilter
				{
					ComparisonOperator = ComparisonOperator.Equal,
					Pid = 22002,
					Value = organizationId,
				},
			});

			return linkerRows.Select(row => new User
			{
				Id = Convert.ToString(row[0]),
				Organization = Convert.ToString(row[1]),
				Name = Convert.ToString(row[2]),
			}).ToList();
		}

		public bool CreateRepository(RepositoryContext context)
		{
			// Init Progress
			CreationProgress?.Invoke(this, new StatusProgressEventArgs("Creating Repository..."));

			// Creating repository
			var repoResult = CreateRepositoryBasic(new CreateRepositoryData
			{
				OrganizationId = context.Organization,
				Name = context.Name,
				Description = context.Description,
				Public = context.Public,
			});
			if (!repoResult.Success)
			{
				CreationProgress?.Invoke(this, new StatusProgressEventArgs(repoResult.Description));
				return false;
			}

			context.Id = repoResult.RepositoryId.FullName;
			CreationProgress?.Invoke(this, new StatusProgressEventArgs("Created Repository"));

			// Create the Sonar Cloud Project if needed (Only the SkylineCommunication organization has the paid plan for Sonar Cloud)
			if (!String.IsNullOrEmpty(context.SonarCloudProjectID) &&
				context.SonarCloudProjectID == "Generate" &&
				context.Organization == "SkylineCommunications")
			{
				CreationProgress?.Invoke(this, new StatusProgressEventArgs("Creating Sonar Cloud Project..."));

				if (CreateSonarCloudProject(context, out var description))
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs($"Successfully created Sonar Cloud Project with id '{context.SonarCloudProjectID}'"));
				}
				else
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs(description));
					return false;
				}
			}

			// Create a Sonar Cloud User Token if needed (Only the SkylineCommunication organization has the paid plan for Sonar Cloud)
			if (!String.IsNullOrEmpty(context.SonarCloudToken) &&
				context.SonarCloudToken == "Generate" &&
				context.Organization == "SkylineCommunications")
			{
				CreationProgress?.Invoke(this, new StatusProgressEventArgs("Creating Sonar Cloud Token..."));

				if (CreateSonarCloudToken(context, out var description))
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs($"Successfully created Sonar Cloud Token '{context.SonarCloudToken}'"));
				}
				else
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs(description));
					return false;
				}
			}

			// Create the files needed for the type of repository
			CreationProgress?.Invoke(this, new StatusProgressEventArgs("Creating needed files..."));
			var pattern = "(\\[(?<content_type>.*)\\]\\s?)?(?<file_name>.*)";
			foreach (var file in context.Files)
			{
				var contentPath = RepositoryContent.RepositoryContentPathMapping[context.WorkflowType];
				if (!contentPath.EndsWith("\\"))
				{
					contentPath += "\\";
				}

				var match = Regex.Match(Path.GetFileName(file), pattern);
				var fileName = match.Groups["file_name"].Value;
				var relFileFolder = Path.GetDirectoryName(file.Replace(contentPath, String.Empty));
				var relFilePath = Path.Combine(relFileFolder, fileName);

				CreationProgress?.Invoke(this, new StatusProgressEventArgs($"Adding '{fileName}'..."));

				CreateRepositoryContentResponse result;
				if (match.Groups["content_type"].Value.ToLower() == "format")
				{
					var fileContent = RepositoryContent.Format(File.ReadAllText(file), context);
					result = CreateRepositoryRawContent(context.Id, relFilePath, fileContent, $"Adding '{fileName}'");
				}
				else
				{
					result = CreateRepositoryContent(context.Id, relFilePath, file, $"Adding '{fileName}'");
				}

				if (!result.Success)
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs($"Failed to create or verify that file '{fileName}', was created."));
					return false;
				}

				CreationProgress?.Invoke(this, new StatusProgressEventArgs($"Successfully added '{fileName}'"));
			}

			// Create workflow
			if(context.WorkflowType != Repositories.WorkflowType.None)
			{
				CreationProgress?.Invoke(this, new StatusProgressEventArgs("Creating Workflow..."));

				// Wait for the public key to be polled
				for(int i = 0; i < 10; i++)
				{
					var keyId = Convert.ToString(element.GetTable(1000).GetRow(context.Id)[15]);
					if (String.IsNullOrEmpty(keyId) || keyId == "-2")
					{
						CreationProgress?.Invoke(this, new StatusProgressEventArgs("Waiting on public keys..."));
						System.Threading.Thread.Sleep(1000);
					}
					else
					{
						break;
					}
				}

				var workflowResult = CreateRepositoryWorkflow(context);
				if (!workflowResult.Success)
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs(workflowResult.Description));
					return false;
				}

				CreationProgress?.Invoke(this, new StatusProgressEventArgs("Successfully added workflow"));
			}

			// Add Teams
			CreationProgress?.Invoke(this, new StatusProgressEventArgs("Adding teams..."));
			foreach (var team in context.Teams)
			{
				var teamResult = AddRepositoryCollaboratorTeam(context, team);
				if (!teamResult.Success)
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs(teamResult.Description));
					return false;
				}

				CreationProgress?.Invoke(this, new StatusProgressEventArgs($"Successfully added '{team.Name}'"));
			}

			// Add Users
			CreationProgress?.Invoke(this, new StatusProgressEventArgs("Adding users..."));
			foreach (var user in context.Users)
			{
				var userResult = AddRepositoryCollaboratorUser(context, user);
				if (!userResult.Success)
				{
					CreationProgress?.Invoke(this, new StatusProgressEventArgs(userResult.Description));
					return false;
				}

				CreationProgress?.Invoke(this, new StatusProgressEventArgs($"Successfully added '{user.Name}'"));
			}

			CreationProgress?.Invoke(this, new StatusProgressEventArgs("Successfully created the repository"));
			return true;
		}

		private CreateRepositoryResponse CreateRepositoryBasic(CreateRepositoryData data)
		{
			var request = new CreateRepositoryRequest
			{
				Data = data,
			};

			try
			{
				var response = interApp.SendSingleResponseMessage<CreateRepositoryResponse>(request);
				return response;
			}
			catch (TimeoutException)
			{
				return new CreateRepositoryResponse
				{
					Success = false,
					Description = "Timeout. Could not verify if the repository was created or not.",
					RepositoryId = default,
					Request = request,
				};
			}
		}

		private CreateRepositoryContentResponse CreateRepositoryContent(string repositoryId, string repoPath, string contentPath, string commitMessage = null)
		{
			var request = new CreateRepositoryContentRequest
			{
				RepositoryId = new RepositoryId(repositoryId.Split('/')[0], repositoryId.Split('/')[1]),
				RepositoryPath = repoPath,
				CommitMessage = commitMessage,
				Data = new CreateContentData
				{
					Method = UpdateMethod.File,
					Path = contentPath,
				},
			};

			try
			{
				var response = interApp.SendSingleResponseMessage<CreateRepositoryContentResponse>(request);
				return response;
			}
			catch (TimeoutException)
			{
				return new CreateRepositoryContentResponse
				{
					Success = false,
					Description = "Timeout. Could not verify if the file was created/updated or not.",
					RepositoryId = default,
					Request = request,
				};
			}
		}

		private CreateRepositoryContentResponse CreateRepositoryRawContent(string repositoryId, string repoPath, string content, string commitMessage = null)
		{
			var request = new CreateRepositoryContentRequest
			{
				RepositoryId = new RepositoryId(repositoryId.Split('/')[0], repositoryId.Split('/')[1]),
				RepositoryPath = repoPath,
				CommitMessage = commitMessage,
				Data = new CreateContentData
				{
					Method = UpdateMethod.Raw,
					Content = content,
				},
			};

			try
			{
				var response = interApp.SendSingleResponseMessage<CreateRepositoryContentResponse>(request);
				return response;
			}
			catch (TimeoutException)
			{
				return new CreateRepositoryContentResponse
				{
					Success = false,
					Description = "Timeout. Could not verify if the file was created/updated or not.",
					RepositoryId = default,
					Request = request,
				};
			}
		}

		private bool CreateSonarCloudProject(RepositoryContext context, out string description)
		{
			using (var client = new HttpClient())
			{
				var uriBuilder = new UriBuilder(Endpoints.SonarCloudProvisionProject);
				var repoId = Convert.ToInt64(element.GetTable(1000).GetRow(context.Id)[17]); // Get the Id column for the repository
				if (repoId <= 0)
				{
					description = $"Invalid Id found for repository '{context.Id}'";
					return false;
				}

				var data = new SonarCloudProvisionProjectData
				{
					InstallationKeys = $"{context.Id}|{repoId}",
					NewCodeDefinitionType = "previous_version",
					NewCodeDefinitionValue = "previous_version",
					Organization = "skylinecommunications",
				};

				uriBuilder.Query = data.ToGetQuery();

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LocalStorage.ReadSonarToken());
				var post = client.PostAsync(new Uri(uriBuilder.ToString()), new StringContent(string.Empty));
				post.Wait();

				var content = post.Result.Content.ReadAsStringAsync();
				content.Wait();

				if (!post.Result.IsSuccessStatusCode)
				{
					description = content.Result;
					return false;
				}

				var result = JsonConvert.DeserializeObject<SonarCloudProjectResult>(content.Result).Projects.SingleOrDefault().ProjectKey;
				context.SonarCloudProjectID = result;

				description = String.Empty;
				return true;
			}
		}

		private bool CreateSonarCloudToken(RepositoryContext context, out string description)
		{
			using (var client = new HttpClient())
			{
				var uriBuilder = new UriBuilder(Endpoints.SonarCloudGenerateToken);

				var data = new SonarCloudGenerateTokenData
				{
					Name = $"Used to Analyze \"{context.Id}\"",
				};

				uriBuilder.Query = data.ToGetQuery();

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LocalStorage.ReadSonarToken());
				var post = client.PostAsync(new Uri(uriBuilder.ToString()), new StringContent(string.Empty));
				post.Wait();

				var content = post.Result.Content.ReadAsStringAsync();
				content.Wait();

				if (!post.Result.IsSuccessStatusCode)
				{
					description = content.Result;
					return false;
				}

				var result = JsonConvert.DeserializeObject<SonarCloudGenerateTokenResult>(content.Result).Token;
				context.SonarCloudToken = result;

				description = String.Empty;
				return true;
			}
		}

		private AddWorkflowResponse CreateRepositoryWorkflow(RepositoryContext context)
		{
			var request = WorkflowFactory.Create(context);

			try
			{
				var response = interApp.SendSingleResponseMessage<AddWorkflowResponse>(request);
				return response;
			}
			catch (TimeoutException)
			{
				return new AddWorkflowResponse
				{
					Success = false,
					Description = "Timeout. Could not verify if the workflow was created or not.",
					Request = (AddWorkflowRequest)request,
				};
			}
		}

		private AddRepositoryCollaboratorResponse AddRepositoryCollaboratorTeam(RepositoryContext context, Team team)
		{
			var request = new AddRepositoryCollaboratorRequest
			{
				RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
				Data = new AddCollaboratorData
				{
					CollaboratorType = CollaboratorType.Team,
					RepositoryOrganization = team.Organization,
					CollaboratorSlug = team.Name,
					Permission = team.Permission,
				},
			};

			try
			{
				var response = interApp.SendSingleResponseMessage<AddRepositoryCollaboratorResponse>(request);
				return response;
			}
			catch (TimeoutException)
			{
				return new AddRepositoryCollaboratorResponse
				{
					Success = false,
					Description = $"Timeout. Could not verify if the user '{team.Name}', was added or not.",
					Request = request,
				};
			}
		}

		private AddRepositoryCollaboratorResponse AddRepositoryCollaboratorUser(RepositoryContext context, User user)
		{
			var request = new AddRepositoryCollaboratorRequest
			{
				RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
				Data = new AddCollaboratorData
				{
					CollaboratorType = CollaboratorType.User,
					RepositoryOrganization = user.Organization,
					CollaboratorSlug = user.Name,
					Permission = user.Permission,
				},
			};

			try
			{
				var response = interApp.SendSingleResponseMessage<AddRepositoryCollaboratorResponse>(request);
				return response;
			}
			catch (TimeoutException)
			{
				return new AddRepositoryCollaboratorResponse
				{
					Success = false,
					Description = $"Timeout. Could not verify if the user '{user.Name}', was added or not.",
					Request = request,
				};
			}
		}
	}
}
