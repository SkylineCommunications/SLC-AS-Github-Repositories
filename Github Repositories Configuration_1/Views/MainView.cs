// Ignore Spelling: Github

namespace Github_Repositories_Configuration_1.Views
{
	using System;

	using Skyline.DataMiner.Automation;

	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class MainView : Dialog<StackPanel>
	{
		public MainView(IEngine engine) : base(engine)
		{
			Title = "Configure the Github Repositories Solution";

			var form = new FormPanel();
			form.Add("Sonar Cloud Token", SonarCloudToken);
			Panel.Add(form);

			var actions = new StackPanel(Direction.Horizontal)
			{
				QuitButton,
				SaveButton,
			};
			Panel.Add(actions);
		}

		public IButton SonarCloudToken { get; } = new Button("Configure...");

		public IButton SaveButton { get; } = new Button("Save");

		public IButton QuitButton { get; } = new Button("Quit");
	}
}
