// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	internal class SonarCloudProvisionProjectData
	{
		public string InstallationKeys { get; set; }

		public string NewCodeDefinitionValue { get; set; }

		public string NewCodeDefinitionType { get; set; }

		public string Organization { get; set; }

		public string ToGetQuery()
		{
			return $"installationKeys={InstallationKeys}&newCodeDefinitionValue={NewCodeDefinitionValue}&newCodeDefinitionType={NewCodeDefinitionType}&organization={Organization}";
		}
	}
}
