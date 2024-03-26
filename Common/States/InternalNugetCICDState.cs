// Ignore Spelling: Nuget

namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class InternalNugetCICDState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public InternalNugetCICDState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.InternalNugetCICD; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromInternalNugetCicd);
				return new ChooseState(helper, instance);
			}

			if (newStatus == Statuses.PrivateRepository)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromInternalNugetCicdToPrivateRepository);
				return new PrivateRepositoryState(helper, instance);
			}

			if (newStatus == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromInternalNugetCicdToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
