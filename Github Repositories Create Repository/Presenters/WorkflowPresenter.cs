// Ignore Spelling: Github Nuget Api

namespace Skyline.DataMiner.Github.Repositories.Presenters
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
	using Skyline.DataMiner.Github.Repositories.Helpers;
	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Github.Repositories.Views;
	using Skyline.DataMiner.Github.Repositories.Views.Explanations;

	public class WorkflowPresenter
	{
		private readonly ScriptContext context;
		private readonly WorkflowView workflowView;

		private readonly ExplanationInputView sonarCloudProjectIdView;
		private readonly ExplanationInputView sonarCloudTokenView;
		private readonly ExplanationInputView dataMinerDeployKeyView;
		private readonly ExplanationInputView dataMinerTokenView;
		private readonly ExplanationInputView githubTokenView;
		private readonly ExplanationInputView nugetApiTokenView;

		private readonly ProgressPresenter progressPresenter;
		private readonly ProgressView progressView;

		private RepositoryContext repositoryContext;
		private List<ExplanationInputView> requiredInputs = new List<ExplanationInputView>();

		public WorkflowPresenter(ScriptContext context, WorkflowView workflowView, GithubModel model)
		{
			this.context = context;
			this.workflowView = workflowView;

			sonarCloudProjectIdView = new SonarCloudProjectIdView(context.Engine);
			sonarCloudProjectIdView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			sonarCloudProjectIdView.BackButton.Pressed += ClearStatus;
			workflowView.SonarCloudProjectIdButton.Pressed += (sender, e) => context.Controller.ShowDialog(sonarCloudProjectIdView);

			sonarCloudTokenView = new SonarCloudTokenView(context.Engine);
			sonarCloudTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			sonarCloudTokenView.BackButton.Pressed += ClearStatus;
			workflowView.SonarCloudTokenButton.Pressed += (sender, e) => context.Controller.ShowDialog(sonarCloudTokenView);

			dataMinerDeployKeyView = new DataMinerDeployKeyView(context.Engine);
			dataMinerDeployKeyView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			dataMinerDeployKeyView.BackButton.Pressed += ClearStatus;
			workflowView.DataMinerDeployKey.Pressed += (sender, e) => context.Controller.ShowDialog(dataMinerDeployKeyView);

			dataMinerTokenView = new DataMinerTokenView(context.Engine);
			dataMinerTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			dataMinerTokenView.BackButton.Pressed += ClearStatus;
			workflowView.DataMinerToken.Pressed += (sender, e) => context.Controller.ShowDialog(dataMinerTokenView);

			githubTokenView = new GithubTokenView(context.Engine);
			githubTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			githubTokenView.BackButton.Pressed += ClearStatus;
			workflowView.GithubToken.Pressed += (sender, e) => context.Controller.ShowDialog(githubTokenView);

			nugetApiTokenView = new NugetApiTokenView(context.Engine);
			nugetApiTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			nugetApiTokenView.BackButton.Pressed += ClearStatus;
			workflowView.NugetApiToken.Pressed += (sender, e) => context.Controller.ShowDialog(nugetApiTokenView);

			progressView = new ProgressView(context.Engine);
			progressView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			progressPresenter = new ProgressPresenter(context, progressView, model);

			workflowView.NextButton.Pressed += NextButton_Pressed;
		}

		public void Load(RepositoryContext repositoryContext)
		{
			this.repositoryContext = repositoryContext;
			var isSkylineRepo = repositoryContext.Organization == "SkylineCommunications";
			Invisible();
			switch (repositoryContext.WorkflowType)
			{
				case null:
					workflowView.Status.Text = "Nothing to do here, you can go to the next page.";
					break;

				case WorkflowType.AutomationScriptCI:
				case WorkflowType.AutomationScriptCICD:
				case WorkflowType.ConnectorCI:
				case WorkflowType.AppPackage:
					if (isSkylineRepo)
					{
						sonarCloudProjectIdView.Input.Text = "Generate";
					}
					else
					{
						workflowView.SonarCloudProjectIdButton.IsVisible = true;
					}

					workflowView.DataMinerToken.IsVisible = true;
					requiredInputs = new List<ExplanationInputView>
					{
						sonarCloudProjectIdView,
						dataMinerTokenView,
					};
					if (!repositoryContext.Public)
					{
						sonarCloudTokenView.Input.Text = isSkylineRepo ? "Generate" : String.Empty;
						if (!isSkylineRepo)
						{
							sonarCloudTokenView.Input.Text = String.Empty;
							workflowView.SonarCloudTokenButton.IsVisible = true;
						}

						requiredInputs.Add(sonarCloudTokenView);
					}

					break;

				case WorkflowType.InternalNugetSolutionCICD:
					sonarCloudProjectIdView.Input.Text = "Generate";
					workflowView.GithubToken.IsVisible = true;
					requiredInputs = new List<ExplanationInputView>
					{
						sonarCloudProjectIdView,
						githubTokenView,
					};
					if (!repositoryContext.Public)
					{
						sonarCloudTokenView.Input.Text = "Generate";
						requiredInputs.Add(sonarCloudTokenView);
					}

					break;

				case WorkflowType.NugetSolutionCICD:
					if (isSkylineRepo)
					{
						sonarCloudProjectIdView.Input.Text = "Generate";
					}
					else
					{
						workflowView.SonarCloudProjectIdButton.IsVisible = true;
					}

					sonarCloudProjectIdView.Input.Text = "Generate";
					workflowView.NugetApiToken.IsVisible = true;
					requiredInputs = new List<ExplanationInputView>
					{
						sonarCloudProjectIdView,
						nugetApiTokenView,
					};
					if (!repositoryContext.Public)
					{
						sonarCloudTokenView.Input.Text = "Generate";
						if (!isSkylineRepo)
						{
							sonarCloudTokenView.Input.Text = String.Empty;
							workflowView.SonarCloudTokenButton.IsVisible = true;
						}

						requiredInputs.Add(sonarCloudTokenView);
					}
					break;

				default:
					throw new NotSupportedException($"This repository type is not supported. [{repositoryContext.WorkflowType}]");
			}
		}

		private void Invisible()
		{
			workflowView.SonarCloudProjectIdButton.IsVisible = false;
			workflowView.SonarCloudTokenButton.IsVisible = false;
			workflowView.DataMinerDeployKey.IsVisible = false;
			workflowView.DataMinerToken.IsVisible = false;
			workflowView.GithubToken.IsVisible = false;
			workflowView.NugetApiToken.IsVisible = false;
		}

		private bool Validate(out string errors)
		{
			var result = true;
			var sb = new StringBuilder();
			foreach (var input in requiredInputs)
			{
				if (input is DataMinerTokenView &&
					repositoryContext.Organization == "SkylineCommunications" &&
					repositoryContext.Public)
				{
					continue;
				}

				if (String.IsNullOrEmpty(input.Input.Text))
				{
					sb.AppendLine($"'{input.Title}' cannot be left empty.");
					result = false;
				}
			}

			errors = sb.ToString();
			return result;
		}

		private void ClearStatus(object sender, EventArgs e)
		{
			workflowView.Status.Text = String.Empty;
		}

		private void NextButton_Pressed(object sender, EventArgs e)
		{
			if (!Validate(out var errors))
			{
				workflowView.Status.Text += errors;
				return;
			}

			repositoryContext.SonarCloudProjectID = sonarCloudProjectIdView.Input.Text;
			repositoryContext.SonarCloudToken = sonarCloudTokenView.Input.Text;
			repositoryContext.DataMinerDeployKey = dataMinerDeployKeyView.Input.Text;
			repositoryContext.DataMinerToken = dataMinerTokenView.Input.Text;
			repositoryContext.GithubToken = githubTokenView.Input.Text;
			repositoryContext.NugetApiKey = nugetApiTokenView.Input.Text;
			progressPresenter.Load(repositoryContext);
			context.Controller.ShowDialog(progressView);
		}
	}
}
