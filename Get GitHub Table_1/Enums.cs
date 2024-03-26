// Ignore Spelling: Github

namespace Get_GitHub_Table_1
{
	using System.ComponentModel;

	public enum GithubTable
	{
		[Description("Repositories")]
		Repositories = 1000,

		[Description("Tags")]
		Tags = 1200,

		[Description("Releases")]
		Releases = 1400,

		[Description("Workflows")]
		Workflows = 1600,

		[Description("Issues")]
		Issues = 2000,

		[Description("Organizations")]
		Organizations = 3000,

		[Description("Poll Manager")]
		PollManager = 21000,
	}
}
