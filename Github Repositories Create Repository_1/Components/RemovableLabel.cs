// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Components
{
	using System;

	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class RemovableLabel<T> : StackPanel
	{
		public RemovableLabel()
		{
			Direction = Direction.Horizontal;
			Add(Label);
			Add(RemoveButton);
		}

		public RemovableLabel(string name, T value)
		{
			Direction = Direction.Horizontal;
			Label.Text = name;
			Value = value;

			Add(Label);
			Add(RemoveButton);
		}

		public T Value { get; private set; }

		public ILabel Label { get; } = new Label(String.Empty);

		public IButton RemoveButton { get; } = new Button("x");
	}
}
