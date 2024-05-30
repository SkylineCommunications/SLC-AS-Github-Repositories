// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Models
{
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.Github.Repositories.Components;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class User : IOptionableWithPermission<User>
	{
		public string Id { get; set; }

		public string Organization { get; set; }

		public string Name { get; set; }

		public PermissionType Permission { get; set; } = PermissionType.Read;

		public Option<User> ToOption() => Option.Create(Name, this);
	}
}
