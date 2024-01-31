// Ignore Spelling: Workflow

namespace Github_Repositories_Add_Workflow_1
{
	using System;

	using Add_Repository_To_Github_Repositories_Element_1.Workflows;

	using Common;

	using Newtonsoft.Json;

	public static class WorkflowFactory
	{
		public static string Create(AddWorkflowInstance instance)
		{
			switch (instance.Type)
			{
				case Common.DomIds.Github_Repositories.Enums.Workflowtype.AutomationScriptCI:
					var ciRequest = new AddAutomationScriptCIWorkflowRequest
					{
						RepositoryId = instance.RepositoryID,
						DataMinerKey = instance.AutomationScriptCI_DataMinerDeployKey,
						SonarCloudProjectID = instance.AutomationScriptCI_SonarCloudProjectID,
					};
					return JsonConvert.SerializeObject(new[] { ciRequest });

				case Common.DomIds.Github_Repositories.Enums.Workflowtype.AutomationScriptCICD:
					var cicdRequest = new AddAutomationScriptCICDWorkflowRequest
					{
						RepositoryId = instance.RepositoryID,
						DataMinerKey = instance.AutomationScriptCICD_DataMinerDeployKey,
						SonarCloudProjectID = instance.AutomationScriptCICD_SonarCloudProjectID,
					};
					return JsonConvert.SerializeObject(new[] { cicdRequest });

				default:
					throw new NotSupportedException("This workflow type is not supported yet.");
			}
		}
	}
}
