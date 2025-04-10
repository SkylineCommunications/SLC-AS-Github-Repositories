// Ignore Spelling: Github

namespace GetGithubTable.Tables
{
	using Skyline.DataMiner.Analytics.GenericInterface;

	internal interface IGithubTable
	{
		int AgentID { get; }

		int ElementID { get; }

		GithubTable Type { get; }

		GQIColumn[] GetColumns();

		GQIPage GetNextPage(GetNextPageInputArgs args);
	}
}
