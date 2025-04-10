// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Components
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class AddableDropDownStackPanel<T> : StackPanel
	{
		private readonly Dictionary<IButton, RemovableLabel<T>> items = new Dictionary<IButton, RemovableLabel<T>>();

		public AddableDropDownStackPanel()
		{
			var addable = new StackPanel(Direction.Horizontal);
			addable.Add(Options);
			addable.Add(AddButton);
			Add(addable);
		}

		public DropDown<T> Options { get; } = new DropDown<T>();

		public IButton AddButton { get; } = new Button("+");

		public IEnumerable<T> Values { get => items.Values.Select(x => x.Value); }

		public void Add(Option<T> item)
		{
			var label = new RemovableLabel<T>(item.Name, item.Value);
			Insert(Count - 1, label);
			label.RemoveButton.Pressed += RemoveButton_Pressed;
			items.Add(label.RemoveButton, label);
		}

		public void AddRange(IEnumerable<Option<T>> options)
		{
			foreach (var option in options)
			{
				var label = new RemovableLabel<T>(option.Name, option.Value);
				Insert(Count - 1, label);
				label.RemoveButton.Pressed += RemoveButton_Pressed;
				items.Add(label.RemoveButton, label);
			}
		}

		public void Clear(IEngine engine = null)
		{
			foreach (var item in items.Values)
			{
				Remove(item);
			}
		}

		private void RemoveButton_Pressed(object sender, EventArgs e)
		{
			var button = (IButton)sender;
			Remove(items[button]);
		}
	}
}
