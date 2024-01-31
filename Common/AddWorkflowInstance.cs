// Ignore Spelling: Workflow

namespace Common
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using Common.DomIds;
	using Common.States;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Net.Sections;

	public class AddWorkflowInstance
	{
		private DomHelper helper;
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
			this.helper = helper;
			instance = new DomInstance
			{
				DomDefinitionId = Github_Repositories.Definitions.AddWorkflow,
			};

			// Set Defaults
			instance.Sections.Add(new Section(Github_Repositories.Sections.ChooseWorkflow.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInAutomationScriptCIInformation.Id));
			instance.Sections.Add(new Section(Github_Repositories.Sections.FillInAutomationScriptCICDInformation.Id));

			instance = helper.DomInstances.Create(instance);
		}

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

					case Github_Repositories.Behaviors.AddWorkflow.Statuses.Completed:
						return new CompletedState(helper, this);

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
