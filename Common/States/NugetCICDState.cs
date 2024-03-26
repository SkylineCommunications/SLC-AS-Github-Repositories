// Ignore Spelling: Nuget

namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class NugetCICDState : IAddWorkflowState
	{
		private readonly DomHelper helper;
		private readonly AddWorkflowInstance instance;

		public NugetCICDState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.NugetCICD; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromNugetCicd);
				return new ChooseState(helper, instance);
			}

			if (newStatus == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromNugetCicdToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
