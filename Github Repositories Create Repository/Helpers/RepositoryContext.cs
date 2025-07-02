// Ignore Spelling: Github Nuget Api

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
	using Skyline.DataMiner.Github.Repositories.Models;

	public class RepositoryContext
	{
		public RepositoryType RepositoryType { get; set; }

		public WorkflowType? WorkflowType { get; set; }

		public string Id { get; set; }

		public string Organization { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool Public { get; set; }

		public string SonarCloudProjectID { get; set; }

		public string SonarCloudToken { get; set; }

		public string SonarCloudBadgeToken { get; set; }

		public string DataMinerToken { get; set; }

		public string DataMinerDeployKey { get; set; }

		public string GithubToken { get; set; }

		public string NugetApiKey { get; set; }

		public List<Team> Teams { get; } = new List<Team>();

		public List<User> Users { get; } = new List<User>();

		public List<string> Files { get; } = new List<string>();
	}
}
