namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class PrivateRepositoryState : IAddWorkflowState
	{
		private readonly DomHelper helper;
		private readonly AddWorkflowInstance instance;

		public PrivateRepositoryState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.PrivateRepository; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromPrivateRepositoryToCompleted);
				return new ChooseState(helper, instance);
			}

			if (newStatus == Statuses.AutomationScriptCI)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromPrivateRepositoryToAutomationScriptCi);
				return new CompletedState(helper, instance);
			}

			if (newStatus == Statuses.AutomationScriptCICD)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromPrivateRepositoryToAutomationScriptCicd);
				return new CompletedState(helper, instance);
			}

			if (newStatus == Statuses.ConnectorCI)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromPrivateRepositoryToConnectorCi);
				return new CompletedState(helper, instance);
			}

			if (newStatus == Statuses.InternalNugetCICD)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromPrivateRepositoryToInternalNugetCicd);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
