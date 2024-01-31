namespace Common.States
{
	using System;

	using Common.DomIds;

	public static class States
	{
		public static Statuses GetState(AddWorkflowInstance instance, string actionId)
		{
			switch (actionId)
			{
				case "to-workflow-information":
					return GetInformationState(instance);

				case "return-to-choose":
					return Statuses.ChooseWorkflow;

				case "add-workflow-to-repository":
					return Statuses.Completed;

				default:
					throw new NotSupportedException("This Workflow type is not supported yet.");
			}
		}

		private static Statuses GetInformationState(AddWorkflowInstance instance)
		{
			switch (instance.Type)
			{
				case Github_Repositories.Enums.Workflowtype.AutomationScriptCI:
					return Statuses.AutomationScriptCI;

				case Github_Repositories.Enums.Workflowtype.AutomationScriptCICD:
					return Statuses.AutomationScriptCICD;

				default:
					throw new NotSupportedException("This Workflow type is not supported yet.");
			}
		}
	}
}
