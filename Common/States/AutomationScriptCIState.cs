namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class AutomationScriptCIState : IAddWorkflowState
	{
		private readonly DomHelper helper;
		private readonly AddWorkflowInstance instance;

		public AutomationScriptCIState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.AutomationScriptCI; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCi);
				return new ChooseState(helper, instance);
			}

			if (newStatus == Statuses.PrivateRepository)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCiToPrivateRepository);
				return new PrivateRepositoryState(helper, instance);
			}

			if (newStatus == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCiToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
