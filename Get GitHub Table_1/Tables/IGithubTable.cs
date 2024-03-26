namespace Get_GitHub_Table_1.Tables
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
