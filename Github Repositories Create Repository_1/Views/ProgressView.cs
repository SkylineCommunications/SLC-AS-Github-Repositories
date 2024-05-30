// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views
{
	using System;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ProgressView : Dialog<StackPanel>
	{
		public ProgressView(IEngine engine) : base(engine)
		{
			Title = "Creating Repository";

			Panel.Add(Status);
			Panel.Add(new WhiteSpace());

			var buttons = new StackPanel(Direction.Horizontal);
			buttons.Add(BackButton);
			buttons.Add(CreateButton);
			Panel.Add(buttons);
		}

		public Label Status { get; } = new Label("Ready to create the repository. Click on create to start setting up the repository.");

		public IButton BackButton { get; } = new Button("Back");

		public IButton CreateButton { get; } = new Button("Create");
	}
}
