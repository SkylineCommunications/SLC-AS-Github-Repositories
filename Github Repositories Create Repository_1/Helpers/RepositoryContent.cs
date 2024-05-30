// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	public static class RepositoryContent
	{
		public static readonly IReadOnlyDictionary<WorkflowType, string> RepositoryContentPathMapping = new Dictionary<WorkflowType, string>
		{
			{ WorkflowType.AutomationScript,          @"C:\Skyline DataMiner\Documents\Github Repositories\Automation Script" },
			{ WorkflowType.Connector,                 @"C:\Skyline DataMiner\Documents\Github Repositories\Connector" },
			{ WorkflowType.InternalNuget,             @"C:\Skyline DataMiner\Documents\Github Repositories\Internal Nuget" },
			{ WorkflowType.Nuget,                     @"C:\Skyline DataMiner\Documents\Github Repositories\Nuget" },
		};

		public static string[] GetFilesByRepositoryType(WorkflowType type)
		{
			if (!RepositoryContentPathMapping.TryGetValue(type, out var contentPath))
			{
				return new string[0];
			}

			string[] files = Directory.GetFiles(contentPath, "*", SearchOption.AllDirectories);
			switch (type)
			{
				case WorkflowType.AutomationScript:
					break;

				case WorkflowType.Connector:
					break;

				case WorkflowType.InternalNuget:
					break;

				case WorkflowType.Nuget:
					break;

				default:
					throw new NotSupportedException("The given repository type is not supported yet.");
			}

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
				if(sonarCloudId == "Generate")
				{
					sonarCloudId = "TODO_SonarCloudProjectId";
				}

				content = content.Replace("{{SonarCloudProjectId}}", sonarCloudId);
			}

			return content;
		}
	}
}
