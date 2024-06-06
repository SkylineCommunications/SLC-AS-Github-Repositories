// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;

	public static class RepositoryContent
	{
		public static readonly IReadOnlyDictionary<RepositoryType, string> RepositoryContentPathMapping = new Dictionary<RepositoryType, string>
		{
			{ RepositoryType.Automation_Script,             @"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },
			{ RepositoryType.Process_Automation_Script,     @"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },
			{ RepositoryType.GQI_Data_Source,               @"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },
			{ RepositoryType.GQI_Operator,					@"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },
			{ RepositoryType.Life_Service_Orchestration,    @"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },
			{ RepositoryType.Profile_Load_Script,           @"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },
			{ RepositoryType.User_Defined_API,              @"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },

			{ RepositoryType.Connector,                     @"C:\Skyline DataMiner\Documents\Github Repositories\Connector" },

			{ RepositoryType.Solution,                      @"C:\Skyline DataMiner\Documents\Github Repositories\Internal Nuget" },
			{ RepositoryType.Nuget,                         @"C:\Skyline DataMiner\Documents\Github Repositories\Nuget" },
		};

		public static string[] GetFilesByRepositoryType(RepositoryType type)
		{
			if (!RepositoryContentPathMapping.TryGetValue(type, out var contentPath))
			{
				return new string[0];
			}

			string[] files = Directory.GetFiles(contentPath, "*", SearchOption.AllDirectories);
			return files;
		}

		public static string Format(string content, RepositoryContext context)
		{
			if (content.Contains("{{Name}}"))
			{
				content = content.Replace("{{Name}}", context.Name);
			}

			if (content.Contains("{{Description}}"))
			{
				content = content.Replace("{{Description}}", context.Description);
			}

			if (content.Contains("{{SonarCloudProjectId}}"))
			{
				var sonarCloudId = context.SonarCloudProjectID;
				if (sonarCloudId == "Generate")
				{
					sonarCloudId = "TODO_SonarCloudProjectId";
				}

				content = content.Replace("{{SonarCloudProjectId}}", sonarCloudId);
			}

			return content;
		}
	}
}
