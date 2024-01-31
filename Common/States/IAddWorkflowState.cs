// Ignore Spelling: Workflow

namespace Common.States
{
	public enum Statuses
	{
		Unknown = -1,
		Completed,
		ChooseWorkflow,
		AutomationScriptCI,
		AutomationScriptCICD,
	}

	public interface IAddWorkflowState
	{
		IAddWorkflowState Transition(Statuses status);
	}
}
