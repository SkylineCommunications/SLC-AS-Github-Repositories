// Ignore Spelling: dms

namespace Get_GitHub_Table_1.Tables
{
	using System;

	using Skyline.DataMiner.Analytics.GenericInterface;

	internal static class TableFactory
	{
		public static IGithubTable GetTable(GQIDMS dms, GithubTable tableType, int agentId, int elementId)
		{
			IGithubTable githubTable = null;
			switch (tableType)
			{
				case GithubTable.Repositories:
					githubTable = new RepositoriesTable(dms, agentId, elementId);
					break;

				case GithubTable.Tags:
					githubTable = new TagsTable(dms, agentId, elementId);
					break;

				case GithubTable.Releases:
					break;

				case GithubTable.Workflows:
					break;

				case GithubTable.Issues:
					break;

				case GithubTable.Organizations:
					break;

				case GithubTable.PollManager:
					break;

				default:
					throw new NotSupportedException("The given table is not supported yet.");
			}

			return githubTable;
		}
	}
}
