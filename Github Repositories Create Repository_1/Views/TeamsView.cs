// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Github.Repositories.Components;
	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class TeamsView : Dialog<StackPanel>
	{
		public TeamsView(IEngine engine) : base(engine)
		{
			Title = "Add Team";

			Panel.Add(Teams);
			Panel.Add(new WhiteSpace());
			Panel.Add(Back);
		}

		public AddableCollaboratorsPanel<Team> Teams { get; } = new AddableCollaboratorsPanel<Team>();

		public new IButton Back { get; } = new Button("Back");
	}
}
