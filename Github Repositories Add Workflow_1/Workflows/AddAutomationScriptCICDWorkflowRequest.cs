// Ignore Spelling: Workflow Github Workflows

namespace Add_Repository_To_Github_Repositories_Element_1.Workflows
{
	public class AddAutomationScriptCICDWorkflowRequest
	{
		public string RepositoryId { get; set; }

		public int Action { get; } = 0;

		public int WorkflowType { get; } = 1;

		public string SonarCloudProjectID { get; set; }

		public string DataMinerKey { get; set; }
	}
}
