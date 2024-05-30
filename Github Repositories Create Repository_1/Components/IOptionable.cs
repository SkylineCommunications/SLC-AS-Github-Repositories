// Ignore Spelling: Github Optionable

namespace Skyline.DataMiner.Github.Repositories.Components
{
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public interface IOptionableWithPermission<T>
	{
		PermissionType Permission { get; set; }

		Option<T> ToOption();
	}
}
