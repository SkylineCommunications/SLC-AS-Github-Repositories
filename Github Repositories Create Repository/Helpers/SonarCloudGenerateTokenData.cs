// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	internal class SonarCloudGenerateTokenData
	{
		public string Name { get; set; }

		public string ToGetQuery()
		{
			return $"name={Name}";
		}
	}
}
