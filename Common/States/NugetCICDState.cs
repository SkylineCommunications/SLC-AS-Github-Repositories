// Ignore Spelling: Nuget

namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class NugetCICDState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public NugetCICDState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.NugetCICD; }

		public IAddWorkflowState Transition(Statuses newSatus)
		{
			if (newSatus == Statuses.ChooseWorkflow)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromNugetCicd);
				return new ChooseState(helper, instance);
			}

			if (newSatus == Statuses.Completed)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromNugetCicdToCompleted);
				return new CompletedState(helper, instance);
			}

			return this;
		}
	}
}
