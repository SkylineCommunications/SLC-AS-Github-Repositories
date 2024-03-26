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

		public Statuses Status { get => Statuses.AutomationScriptCICD; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCicd);
				return new ChooseState(helper, instance);
			}

			if (newStatus == Statuses.PrivateRepository)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCicdToPrivateRepository);
				return new PrivateRepositoryState(helper, instance);
			}

			if (newStatus == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromAutomationScriptCicdToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
