// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;

	public static class TopicFactory
	{
		public static IEnumerable<RepositoryTopic> Create(RepositoryContext context)
		{
			switch (context.RepositoryType)
			{
				case RepositoryType.Automation_Script:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Automation_Script,
					};

				case RepositoryType.ChatOps:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_ChatOps,
					};

				case RepositoryType.Connector:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Connector,
					};

				case RepositoryType.Dashboard:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Dashboard,
					};

				case RepositoryType.DIS_Macro:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_DIS_Macro,
					};

				case RepositoryType.Doc:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Doc,
					};

				case RepositoryType.Function:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Function,
					};

				case RepositoryType.GQI_Data_Source:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_GQI_Data_Source,
					};

				case RepositoryType.GQI_Operator:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_GQI_Operator,
					};

				case RepositoryType.Life_Service_Orchestration:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Life_Service_Orchestration,
					};

				case RepositoryType.Nuget:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Nuget,
					};

				case RepositoryType.Process_Automation_Script:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Process_Automation_Script,
					};

				case RepositoryType.Profile_Load_Script:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Profile_Load_Script,
					};

				case RepositoryType.Solution:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Solution,
					};

				case RepositoryType.User_Defined_API:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_User_Defined_API,
					};

				case RepositoryType.Visio:
					return new[]
					{
						RepositoryTopic.DataMiner,
						RepositoryTopic.DataMiner_Visio,
					};

				default:
					return new[]
					{
						RepositoryTopic.DataMiner,
					};
			}
		}
	}
}
