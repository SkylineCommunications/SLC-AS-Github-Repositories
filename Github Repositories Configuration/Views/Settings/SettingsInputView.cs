// Ignore Spelling: Github

namespace GithubRepositoriesConfiguration.Views.Settings
{
	using System;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	internal class SettingsInputView : Dialog<StackPanel>
	{
		public SettingsInputView(IEngine engine, string title) : base(engine)
		{
			Title = title;
			Panel.Add(Explanation);
			Panel.Add(Input);
			Panel.Add(new WhiteSpace());
			Panel.Add(BackButton);
		}

		public ILabel Explanation { get; } = new Label(String.Empty);

		public ITextBox Input { get; } = new TextBox();

		public IButton BackButton { get; } = new Button("Back");
	}
}
