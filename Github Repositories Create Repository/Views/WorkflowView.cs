// Ignore Spelling: Github Nuget Api

namespace Skyline.DataMiner.Github.Repositories.Views
{
	using System;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class WorkflowView : Dialog<StackPanel>
	{
		public WorkflowView(IEngine engine) : base(engine)
		{
			Title = "Workflow Details";

			var form = new FormPanel();
			form.Add("Sonar Cloud Project ID", SonarCloudProjectIdButton);
			form.Add("Sonar Cloud Token", SonarCloudTokenButton);
			form.Add("DataMiner Deploy Key", DataMinerDeployKey);
			form.Add("DataMiner Token", DataMinerToken);
			form.Add("Github Token", GithubToken);
			form.Add("Nuget API Token", NugetApiToken);
			Panel.Add(form);
			Panel.Add(new WhiteSpace());
			Panel.Add(Status);
			var actions = new StackPanel(Direction.Horizontal);
			actions.Add(BackButton);
			actions.Add(NextButton);
			Panel.Add(actions);
		}

		public IButton SonarCloudProjectIdButton{ get; } = new Button("Edit...");

		public IButton SonarCloudTokenButton { get; } = new Button("Edit...");

		public IButton DataMinerDeployKey { get; } = new Button("Edit...");

		public IButton DataMinerToken { get; } = new Button("Edit...");

		public IButton GithubToken { get; } = new Button("Edit...");

		public IButton NugetApiToken { get; } = new Button("Edit...");

		public ILabel Status { get; } = new Label(String.Empty);

		public IButton BackButton { get; } = new Button("Back");

		public IButton NextButton { get; } = new Button("Next");
	}
}
