// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Components
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class AddableCollaboratorsPanel<T> : StackPanel
		where T : IOptionableWithPermission<T>
	{
		private readonly StackPanel items = new StackPanel(Direction.Vertical);

		public AddableCollaboratorsPanel()
		{
			Permissions.Options.AddRange(((PermissionType[])Enum.GetValues(typeof(PermissionType))).Select(x => Option.Create(Enum.GetName(typeof(PermissionType), x), x)));

			// 2 span for name, 2 span for permission, 1 span for remove
			Add(items);
			Add(new WhiteSpace());
			var actions = new StackPanel(Direction.Horizontal);
			actions.Add(Collaborators, 2);
			actions.Add(Permissions, 2);
			actions.Add(AddButton, 1);
			Add(actions);

			AddButton.Pressed += AddButton_Pressed;
		}

		public DropDown<T> Collaborators { get; } = new DropDown<T>();

		public DropDown<PermissionType> Permissions { get; } = new DropDown<PermissionType>();

		public IButton AddButton { get; } = new Button("+");

		public StackPanel Items { get => items; }

		public IEnumerable<T> Values
		{
			get
			{
				var result = items.Cast<StackPanel>()
					.Select(x => x.FirstOrDefault())
					.Where(x => x != null)
					.Cast<LabelWithValue<T>>()
					.Where(x => x != null)
					.Select(x => x.Value);
				return result;
			}
		}

		private void AddItem(T item, PermissionType permission)
		{
			item.Permission = permission;
			var row = new StackPanel(Direction.Horizontal);
			row.Add(new LabelWithValue<T>(item.ToOption()), 2);
			var dropdown = new DropDown<PermissionType>();
			dropdown.Options.AddRange(((PermissionType[])Enum.GetValues(typeof(PermissionType))).Select(x => Option.Create(Enum.GetName(typeof(PermissionType), x), x)));
			dropdown.Selected = Option.Create(Enum.GetName(typeof(PermissionType), permission), permission);
			dropdown.Changed += (sender, e) => item.Permission = e.SelectedOption.Value;
			row.Add(dropdown, 2);
			var removeButton = new Button("x");
			removeButton.Pressed += (sender, e) => items.Remove(row);
			row.Add(removeButton, 1);
			items.Add(row);
		}

		private void AddButton_Pressed(object sender, EventArgs e)
		{
			var item = Collaborators.SelectedValue;
			var permission = Permissions.SelectedValue;

			if(!Collaborators.Options.Any())
			{
				return;
			}

			if (!Values.Any(x => x?.ToOption()?.Name == item?.ToOption()?.Name))
			{
				AddItem(item, permission);
			}
		}
	}
}
