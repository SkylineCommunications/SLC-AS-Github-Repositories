// Ignore Spelling: Github

namespace Github_Repositories_Configuration_1.Models
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
