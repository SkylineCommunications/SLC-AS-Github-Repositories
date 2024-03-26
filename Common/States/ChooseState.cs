namespace Common.States
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class ChooseState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public ChooseState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.ChooseWorkflow; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.AutomationScriptCI)
			{
				instance.Type = Github_Repositories.Enums.Workflowtype.AutomationScriptCI;
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.ToAutomationScriptCi);
				return new AutomationScriptCIState(helper, instance);
			}

			if (newStatus == Statuses.AutomationScriptCICD)
			{
				instance.Type = Github_Repositories.Enums.Workflowtype.AutomationScriptCICD;
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.ToAutomationScriptCicd);
				return new AutomationScriptCICDState(helper, instance);
			}

			if (newStatus == Statuses.ConnectorCI)
			{
				instance.Type = Github_Repositories.Enums.Workflowtype.ConnectorCI;
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.ToConnectorCi);
				return new ConnectorCIState(helper, instance);
			}

			if (newStatus == Statuses.NugetCICD)
			{
				instance.Type = Github_Repositories.Enums.Workflowtype.NugetSolutionCICD;
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.ToNugetCicd);
				return new NugetCICDState(helper, instance);
			}

			if (newStatus == Statuses.InternalNugetCICD)
			{
				instance.Type = Github_Repositories.Enums.Workflowtype.InternalNugetSolutionCICD;
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.ToInternalNugetCicd);
				return new InternalNugetCICDState(helper, instance);
			}

			return this;
		}
	}
}
