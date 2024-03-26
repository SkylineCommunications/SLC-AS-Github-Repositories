// Ignore Spelling: Workflow

namespace Common.States
{
	public enum Statuses
	{
		Unknown = -1,
		Result,
		Completed,
		ChooseWorkflow,
		PrivateRepository,
		AutomationScriptCI,
		AutomationScriptCICD,
		ConnectorCI,
		NugetCICD,
		InternalNugetCICD,
	}

	public interface IAddWorkflowState
	{
		Statuses Status { get; }

		IAddWorkflowState Transition(Statuses newStatus);
	}
}
