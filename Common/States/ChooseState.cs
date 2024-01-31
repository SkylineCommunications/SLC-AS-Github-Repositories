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

		public IAddWorkflowState Transition(Statuses status)
		{
			if (status == Statuses.AutomationScriptCI)
			{
				instance.Type = Github_Repositories.Enums.Workflowtype.AutomationScriptCI;
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.ToAutomationScriptCi);
				return new AutomationScriptCIState(helper, instance);
			}

			if (status == Statuses.AutomationScriptCICD)
			{
				instance.Type = Github_Repositories.Enums.Workflowtype.AutomationScriptCICD;
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.ToAutomationScriptCicd);
				return new AutomationScriptCICDState(helper, instance);
			}

			return this;
		}
	}
}
