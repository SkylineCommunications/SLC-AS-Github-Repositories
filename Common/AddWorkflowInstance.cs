// Ignore Spelling: Workflow Nuget Api Github

namespace Common
{
	using System;
	using System.Linq;

	using Common.DomIds;
	using Common.States;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Net.Sections;

	public class AddWorkflowInstance
	{
		private readonly DomHelper helper;
		private DomInstance instance;

		public AddWorkflowInstance(DomHelper helper, DomInstanceId instanceId)
		{
			this.helper = helper;
			var instances = helper.DomInstances.Read(DomInstanceExposers.DomDefinitionId.Equal(Github_Repositories.Definitions.AddWorkflow.Id));
			var exists = instances.Find(x => x.ID.Id == instanceId.Id);
			if (exists != null)
			{
				this.instance = exists;
			}
			else
			{
				throw new ArgumentException(nameof(instanceId), $"Could not find an instance with id: {instanceId}");
			}
		}

		public AddWorkflowInstance(DomHelper helper)
		{
			var instances = helper.DomInstances.Read(DomInstanceExposers.DomDefinitionId.Equal(Github_Repositories.Definitions.AddWorkflow.Id));
			foreach (var inst in instances)
			{
				helper.DomInstances.Delete(inst);
			}

			this.helper = helper;
			instance = new DomInstance
			{
				DomDefinitionId = Github_Repositories.Definitions.AddWorkflow,
			};

			// Set Defaults
			instance.Sections.Add(new Section(Github_Repositories.Sections.ChooseWorkflow.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInConnectorCIInformation.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInNugetCICDInformation.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInInternalNugetCICDInformation.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInPrivateRepositoryInformation.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.Result.Id));

			instance = helper.DomInstances.Create(instance);
		}

		#region Properties
		#region General
		public int DataMinerID
		{
			get => Convert.ToInt32(instance.GetFieldValue<double>(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.DataminerID)?.Value ?? -1);
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.DataminerID, Convert.ToDouble(value));
		}

		public int ElementID
		{
			get => Convert.ToInt32(instance.GetFieldValue<double>(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.ElementID)?.Value ?? -1);
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.ElementID, Convert.ToDouble(value));
		}

		public string RepositoryID
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.RepositoryID)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.RepositoryID, value);
		}
		#endregion

		#region Automation Script CI
		public string AutomationScriptCI_SonarCloudProjectID
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCIInformation.SonarCloudProjectID)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCIInformation.SonarCloudProjectID, value);
		}

		public string AutomationScriptCI_DataMinerDeployKey
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCIInformation.DataminerDeployKey)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCIInformation.DataminerDeployKey, value);
		}

		public bool AutomationScriptCI_IsPrivateRepository
		{
			get => instance.GetFieldValue<bool>(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCIInformation.IsPrivateRepository)?.Value ?? false;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCIInformation.IsPrivateRepository, value);
		}
		#endregion

		#region Automation Script CICD
		public string AutomationScriptCICD_SonarCloudProjectID
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCICDInformation.SonarCloudProjectID)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCICDInformation.SonarCloudProjectID, value);
		}

		public string AutomationScriptCICD_DataMinerDeployKey
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCICDInformation.DataminerDeployKey)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCICDInformation.DataminerDeployKey, value);
		}

		public bool AutomationScriptCICD_IsPrivateRepository
		{
			get => instance.GetFieldValue<bool>(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCICDInformation.IsPrivateRepository)?.Value ?? false;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id, Github_Repositories.Sections.FillInAutomationScriptCICDInformation.IsPrivateRepository, value);
		}
		#endregion

		#region Connector CI
		public string ConnectorCI_SonarCloudProjectID
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInConnectorCIInformation.Id, Github_Repositories.Sections.FillInConnectorCIInformation.SonarCloudProjectID)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInConnectorCIInformation.Id, Github_Repositories.Sections.FillInConnectorCIInformation.SonarCloudProjectID, value);
		}

		public string ConnectorCI_DataMinerDeployKey
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInConnectorCIInformation.Id, Github_Repositories.Sections.FillInConnectorCIInformation.DataminerDeployKey)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInConnectorCIInformation.Id, Github_Repositories.Sections.FillInConnectorCIInformation.DataminerDeployKey, value);
		}

		public bool ConnectorCI_IsPrivateRepository
		{
			get => instance.GetFieldValue<bool>(Github_Repositories.Sections.FillInConnectorCIInformation.Id, Github_Repositories.Sections.FillInConnectorCIInformation.IsPrivateRepository)?.Value ?? false;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInConnectorCIInformation.Id, Github_Repositories.Sections.FillInConnectorCIInformation.IsPrivateRepository, value);
		}
		#endregion

		#region Nuget CICD
		public string NugetCICD_SonarCloudProjectID
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInNugetCICDInformation.Id, Github_Repositories.Sections.FillInNugetCICDInformation.SonarCloudProjectID)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInNugetCICDInformation.Id, Github_Repositories.Sections.FillInNugetCICDInformation.SonarCloudProjectID, value);
		}

		public string NugetCICD_NugetApiKey
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInNugetCICDInformation.Id, Github_Repositories.Sections.FillInNugetCICDInformation.NugetAPIKey)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInNugetCICDInformation.Id, Github_Repositories.Sections.FillInNugetCICDInformation.NugetAPIKey, value);
		}
		#endregion

		#region Internal Nuget CICD
		public string InternalNugetCICD_SonarCloudProjectID
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInInternalNugetCICDInformation.Id, Github_Repositories.Sections.FillInInternalNugetCICDInformation.SonarCloudProjectID)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInInternalNugetCICDInformation.Id, Github_Repositories.Sections.FillInInternalNugetCICDInformation.SonarCloudProjectID, value);
		}

		public string InternalNugetCICD_GithubToken
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInInternalNugetCICDInformation.Id, Github_Repositories.Sections.FillInInternalNugetCICDInformation.GithubToken)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInInternalNugetCICDInformation.Id, Github_Repositories.Sections.FillInInternalNugetCICDInformation.GithubToken, value);
		}

		public bool InternalNugetCICD_IsPrivateRepository
		{
			get => instance.GetFieldValue<bool>(Github_Repositories.Sections.FillInInternalNugetCICDInformation.Id, Github_Repositories.Sections.FillInInternalNugetCICDInformation.IsPrivateRepository)?.Value ?? false;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInInternalNugetCICDInformation.Id, Github_Repositories.Sections.FillInInternalNugetCICDInformation.IsPrivateRepository, value);
		}
		#endregion

		#region Private Repository Information
		public string PrivateSonarCloudToken
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.FillInPrivateRepositoryInformation.Id, Github_Repositories.Sections.FillInPrivateRepositoryInformation.PrivateSonarCloudToken)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.FillInPrivateRepositoryInformation.Id, Github_Repositories.Sections.FillInPrivateRepositoryInformation.PrivateSonarCloudToken, value);
		}
		#endregion

		#region Result
		public string ResultMessage
		{
			get => instance.GetFieldValue<string>(Github_Repositories.Sections.Result.Id, Github_Repositories.Sections.Result.ResultMessage)?.Value ?? String.Empty;
			set => instance.AddOrUpdateFieldValue(Github_Repositories.Sections.Result.Id, Github_Repositories.Sections.Result.ResultMessage, value);
		}
		#endregion
		#endregion

		public Github_Repositories.Enums.Workflowtype Type
		{
			get => (Github_Repositories.Enums.Workflowtype)instance.GetFieldValue<int>(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.Workflow)?.Value;
			set
			{
				instance.AddOrUpdateFieldValue(Github_Repositories.Sections.ChooseWorkflow.Id, Github_Repositories.Sections.ChooseWorkflow.Workflow, Convert.ToInt32(value));
			}
		}

		public IAddWorkflowState Status
		{
			get
			{
				var state = instance.StatusId;
				switch (state)
				{
					case Github_Repositories.Behaviors.AddWorkflow.Statuses.ChooseWorkflow:
						return new ChooseState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.FillInAutomationScriptCIInformation:
						return new AutomationScriptCIState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.FillInAutomationScriptCICDInformation:
						return new AutomationScriptCICDState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.FillInConnectorCIInformation:
						return new ConnectorCIState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.FillInNugetCICDInformation:
						return new NugetCICDState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.FillInInternalNugetCICDInformation:
						return new InternalNugetCICDState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.FillInPrivateRepositoryInformation:
						return new PrivateRepositoryState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.Completed:
						return new CompletedState(helper, this);

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.Result:
						return new ResultState(helper, this);

					default:
						throw new NotSupportedException("The current status ID is not supported yet");
				}
			}
		}

		public DomInstance Transition(DomHelper helper, string transitionId)
		{
			instance = helper.DomInstances.DoStatusTransition(instance.ID, transitionId);
			return instance;
		}

		public DomInstance Save(DomHelper helper)
		{
			// Update existing instance
			instance = helper.DomInstances.Update(instance);
			return instance;
		}
	}
}
