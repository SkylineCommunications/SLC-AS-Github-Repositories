// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views
{
	using System;
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class MainView : Dialog<StackPanel>
	{
		public MainView(IEngine engine) : base(engine)
		{
			Title = "Create Repository";

			this.RepositoryType.Options.AddRange(
				Enum.GetValues(typeof(RepositoryType))
					.Cast<RepositoryType>()
					.Select(x => Option.Create(x.FriendlyDescription(), x)));

			// Excluding public nugets, because you need permission for those.
			this.WorkflowType.Options.Add(Option.Create<WorkflowType?>("None", null));
			this.WorkflowType.Options.AddRange(
				Enum.GetValues(typeof(WorkflowType))
					.Cast<WorkflowType>()
					.Where(x => x != ConnectorAPI.Github.Repositories.InterAppMessages.Workflows.WorkflowType.NugetSolutionCICD)
					.Select(x => Option.Create<WorkflowType?>(x.FriendlyDescription(), x)));

			var form = new FormPanel();
			form.Add("Repository WorkflowType", RepositoryType);
			form.Add("Repository Name", Name);
			form.Add("Repository Description", Description);
			form.Add("Workflow WorkflowType", WorkflowType);
			form.Add("Public", Public);
			form.Add("Teams", Teams);
			form.Add("Users", Users);
			Panel.Add(form);
			var actions = new StackPanel(Direction.Horizontal);
			actions.Add(QuitButton);
			actions.Add(NextButton);
			Panel.Add(actions);
		}

		public DropDown<RepositoryType> RepositoryType { get; } = new DropDown<RepositoryType>();

		public ITextBox Name { get; } = new TextBox();

		public ITextBox Description { get; } = new TextBox();

		public ICheckBox Public { get; } = new CheckBox { IsChecked = false };

		public DropDown<WorkflowType?> WorkflowType { get; } = new DropDown<WorkflowType?>();

		public IButton Teams { get; } = new Button("Add...");

		public IButton Users { get; } = new Button("Add...");

		public IButton NextButton { get; } = new Button("Next");

		public IButton QuitButton { get; } = new Button("Quit");
	}
}
