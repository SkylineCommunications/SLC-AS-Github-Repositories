// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views
{
	using System;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ExplanationInputView : Dialog<StackPanel>
	{
		public ExplanationInputView(IEngine engine, string title) : base(engine)
		{
			Title = title;
			Panel.Add(Explanation);
			//Panel.Add(Link);
			Panel.Add(Input);
			Panel.Add(new WhiteSpace());
			Panel.Add(BackButton);
		}

		public ILabel Explanation { get; } = new Label(String.Empty);

		public IButton Link { get; } = new Button("Link");

		public ITextBox Input { get; } = new TextBox();

		public IButton BackButton { get; } = new Button("Back");
	}
}
