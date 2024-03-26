namespace Common.States
{
	using Common.DomIds;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

	public class CompletedState : IAddWorkflowState
	{
		private DomHelper helper;
		private AddWorkflowInstance instance;

		public CompletedState(DomHelper helper, AddWorkflowInstance instance)
		{
			this.helper = helper;
			this.instance = instance;
		}

		public Statuses Status { get => Statuses.Completed; }

		public IAddWorkflowState Transition(Statuses newStatus)
		{
			if (newStatus == Statuses.Result)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromCompletedToResult);
				return new ResultState(helper, instance);
			}

			if (newStatus == Statuses.AutomationScriptCI)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromCompletedToAutomationScriptCi);
				return new AutomationScriptCIState(helper, instance);
			}

			if (newStatus == Statuses.AutomationScriptCICD)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromCompletedToAutomationScriptCicd);
				return new AutomationScriptCICDState(helper, instance);
			}

			if (newStatus == Statuses.ConnectorCI)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromCompletedToConnectorCi);
				return new ConnectorCIState(helper, instance);
			}

			if (newStatus == Statuses.NugetCICD)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromCompletedToNugetCicd);
				return new NugetCICDState(helper, instance);
			}

			if (newStatus == Statuses.InternalNugetCICD)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromCompletedToInternalNugetCicd);
				return new InternalNugetCICDState(helper, instance);
			}

			if (newStatus == Statuses.PrivateRepository)
			{
				instance.Transition(helper, Github_Repositories.Behaviors.AddWorkflow.Transitions.FromCompletedToPrivateRepository);
				return new PrivateRepositoryState(helper, instance);
			}

			return this;
		}
	}
}
