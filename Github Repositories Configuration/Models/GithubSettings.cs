// Ignore Spelling: Github

namespace GithubRepositoriesConfiguration.Models
{
	using System.IO;

	public static class GithubSettings
	{
		public static string ReadSonarCloudToken()
		{
			return File.ReadAllText(Constants.SonarCloudTokenPath);
		}

		public static void WriteSonarCloudToken(string token)
		{
			File.WriteAllText(Constants.SonarCloudTokenPath, token);
		}
	}
}
