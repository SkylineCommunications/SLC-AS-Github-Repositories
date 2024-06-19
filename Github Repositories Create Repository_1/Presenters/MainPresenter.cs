// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Presenters
{
	using System;
	using System.Linq;
	using System.Text.RegularExpressions;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories;
	using Skyline.DataMiner.Github.Repositories.Helpers;
	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Github.Repositories.Views;

	public class MainPresenter
	{
		private readonly ScriptContext context;

		private readonly MainView mainView;
		private readonly TeamsView teamsView;
		private readonly UsersView usersView;
		private readonly WorkflowView workflowView;

		private readonly TeamsPresenter teamsPresenter;
		private readonly UsersPresenter usersPresenter;
		private readonly WorkflowPresenter workflowPresenter;

		public MainPresenter(ScriptContext context, MainView mainView, GithubModel model)
		{
			this.context = context;
			this.mainView = mainView;
			this.teamsView = new TeamsView(mainView.Engine);
			this.usersView = new UsersView(mainView.Engine);
			this.workflowView = new WorkflowView(mainView.Engine);

			this.teamsPresenter = new TeamsPresenter(context, mainView, teamsView, model);
			this.usersPresenter = new UsersPresenter(context, mainView, usersView, model);
			this.workflowPresenter = new WorkflowPresenter(context, workflowView, model);

			mainView.Teams.Pressed += Teams_Pressed;
			mainView.Users.Pressed += Users_Pressed;
			mainView.NextButton.Pressed += Next_Pressed;
			workflowView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(mainView);
			mainView.QuitButton.Pressed += (sender, e) => mainView.Engine.ExitSuccess("User aborted script.");
		}

		private void Teams_Pressed(object sender, EventArgs e)
		{
			teamsPresenter.Load(context.OrganizationId);
			context.Controller.ShowDialog(teamsView);
		}

		private void Users_Pressed(object sender, EventArgs e)
		{
			usersPresenter.Load(context.OrganizationId);
			context.Controller.ShowDialog(usersView);
		}

		private void Next_Pressed(object sender, EventArgs e)
		{
			if (!Validate())
			{
				return;
			}

			var repoContext = new RepositoryContext
			{
				RepositoryType = mainView.RepositoryType.SelectedValue,
				WorkflowType = mainView.WorkflowType.SelectedValue,
				Organization = context.OrganizationId,
				Name = mainView.Name.Text,
				Description = mainView.Description.Text,
				Public = mainView.Public.IsChecked,
			};
			repoContext.Teams.AddRange(teamsView.Teams.Values);
			repoContext.Users.AddRange(usersView.Users.Values);
			repoContext.Files.AddRange(RepositoryContent.GetFilesByRepositoryType(mainView.RepositoryType.SelectedValue));

			workflowPresenter.Load(repoContext);
			context.Controller.ShowDialog(workflowView);
		}

		private bool Validate()
		{
			// Check for Skyline Guidelines in the name
			var pattern1 = $"([a-zA-Z]+)-({mainView.RepositoryType.SelectedValue.GetTypesInitials()})-(.*)";
			var match = Regex.Match(mainView.Name.Text, pattern1);

			context.Engine.GenerateInformation(match.Success.ToString());
			if(!match.Success)
			{
				mainView.Name.ValidationState = UIValidationState.Invalid;
				mainView.Name.ValidationText = $"The name of a '{mainView.RepositoryType.SelectedValue.FriendlyDescription()}' repository should be in the following format: [Customer Initial]-{mainView.RepositoryType.SelectedValue.GetTypesInitials()}-[Name]";
				return false;
			}

			// Check for Github Guidelines in the name
			var pattern2 = @"^[a-zA-Z0-9]+(?:-[a-zA-Z0-9]+)*$";
			if (!Regex.IsMatch(mainView.Name.Text, pattern2))
			{
				mainView.Name.ValidationState = UIValidationState.Invalid;
				mainView.Name.ValidationText = "The name contains invalid characters. It can only contain alphanumeric characters or hyphens (-) and it cannot start or end with a hyphen.";
				return false;
			}

			return true;
		}
	}
}
