// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Models
{
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.Github.Repositories.Components;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class Team : IOptionableWithPermission<Team>
	{
		public string Id { get; set; }

		public string Organization { get; set; }

		public string Name { get; set; }

		public PermissionType Permission { get; set; } = PermissionType.Read;

		public Option<Team> ToOption() => Option.Create(Name, this);
	}
}
