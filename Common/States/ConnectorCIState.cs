namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class ConnectorCIState : IAddWorkflowState
	{
		private readonly DomHelper helper;
		private readonly AddWorkflowInstance instance;

		public ConnectorCIState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.ConnectorCI; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromConnectorCi);
				return new ChooseState(helper, instance);
			}

			if (newStatus == Statuses.PrivateRepository)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromConnectorCiToPrivateRepository);
				return new PrivateRepositoryState(helper, instance);
			}

			if (newStatus == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromConnectorCiToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
