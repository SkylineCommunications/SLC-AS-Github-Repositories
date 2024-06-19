namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	internal class SonarCloudProjectComponentData
	{
		public string ProjectId { get; set; }

		public string ToGetQuery()
		{
			return $"component={ProjectId}";
		}
	}
}
