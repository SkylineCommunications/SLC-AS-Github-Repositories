// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Presenters
{
	using System.Linq;

	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Github.Repositories.Views;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class TeamsPresenter
	{
		private readonly TeamsView teamsView;
		private readonly GithubModel model;

		public TeamsPresenter(ScriptContext context, MainView mainView, TeamsView teamsView, GithubModel model)
		{
			this.teamsView = teamsView;
			this.model = model;

			teamsView.Back.Pressed += (sender, e) => context.Controller.ShowDialog(mainView);
		}

		public void Load(string organizationId)
		{
			var teams = model.GetTeamsByOrganization(organizationId);
			teamsView.Teams.Collaborators.Options.Clear();
			teamsView.Teams.Collaborators.Options.AddRange(teams.Select(x => Option.Create(x.Name, x)));
		}
	}
}
