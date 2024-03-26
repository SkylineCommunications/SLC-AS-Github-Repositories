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
				case Github_Repositories.Behaviors.AddWorkflow.Actions.ToWorkflowInformation:
					return GetInformationState(instance);

				case Github_Repositories.Behaviors.AddWorkflow.Actions.ReturnToChoose:
					return Statuses.ChooseWorkflow;

				case Github_Repositories.Behaviors.AddWorkflow.Actions.ToNextOrComplete:
					return GetNextOrComplete(instance);

				case Github_Repositories.Behaviors.AddWorkflow.Actions.AddWorkflowToRepository:
					return Statuses.Result;

				default:
					throw new NotSupportedException("This Workflow type is not supported yet.");
			}
		}

		private static Statuses GetNextOrComplete(AddWorkflowInstance instance)
		{
			if(instance.Status is PrivateRepositoryState)
			{
				return Statuses.Completed;
			}

			switch (instance.Type)
			{
				case Github_Repositories.Enums.Workflowtype.AutomationScriptCI:
					if (instance.AutomationScriptCI_IsPrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					break;

				case Github_Repositories.Enums.Workflowtype.AutomationScriptCICD:
					if (instance.AutomationScriptCICD_IsPrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					break;

				case Github_Repositories.Enums.Workflowtype.ConnectorCI:
					if (instance.ConnectorCI_IsPrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					break;

				case Github_Repositories.Enums.Workflowtype.InternalNugetSolutionCICD:
					if (instance.InternalNugetCICD_IsPrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					break;

				default:
					throw new NotSupportedException("This Workflow type is not supported yet.");
			}

			return Statuses.Completed;
		}

		private static Statuses GetInformationState(AddWorkflowInstance instance)
		{
			switch (instance.Type)
			{
				case Github_Repositories.Enums.Workflowtype.AutomationScriptCI:
					if (instance.AutomationScriptCI_IsPrivateRepository && instance.Status.Status != Statuses.PrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					return Statuses.AutomationScriptCI;

				case Github_Repositories.Enums.Workflowtype.AutomationScriptCICD:
					if (instance.AutomationScriptCICD_IsPrivateRepository && instance.Status.Status != Statuses.PrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					return Statuses.AutomationScriptCICD;

				case Github_Repositories.Enums.Workflowtype.ConnectorCI:
					if (instance.ConnectorCI_IsPrivateRepository && instance.Status.Status != Statuses.PrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					return Statuses.ConnectorCI;

				case Github_Repositories.Enums.Workflowtype.NugetSolutionCICD:
					return Statuses.NugetCICD;

				case Github_Repositories.Enums.Workflowtype.InternalNugetSolutionCICD:
					if (instance.InternalNugetCICD_IsPrivateRepository && instance.Status.Status != Statuses.PrivateRepository)
					{
						return Statuses.PrivateRepository;
					}

					return Statuses.InternalNugetCICD;

				default:
					throw new NotSupportedException("This Workflow type is not supported yet.");
			}
		}
	}
}
