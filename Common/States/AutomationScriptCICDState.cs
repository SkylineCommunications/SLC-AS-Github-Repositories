namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class AutomationScriptCICDState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public AutomationScriptCICDState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public IAddWorkflowState Transition(Statuses status)
		{
			if (status == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCicd);
				return new ChooseState(helper, instance);
			}

			if (status == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCicdToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
