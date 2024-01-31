namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class AutomationScriptCIState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public AutomationScriptCIState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public IAddWorkflowState Transition(Statuses status)
		{
			if (status == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCi);
				return new ChooseState(helper, instance);
			}

			if (status == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCiToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
