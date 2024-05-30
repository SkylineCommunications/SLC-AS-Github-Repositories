// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Github.Repositories.Components;
	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class UsersView : Dialog<StackPanel>
	{
		public UsersView(IEngine engine) : base(engine)
		{
			Title = "Add User";

			Panel.Add(Users);
			Panel.Add(new WhiteSpace());
			Panel.Add(Back);
		}

		public AddableCollaboratorsPanel<User> Users { get; } = new AddableCollaboratorsPanel<User>();

		public new IButton Back { get; } = new Button("Back");
	}
}
