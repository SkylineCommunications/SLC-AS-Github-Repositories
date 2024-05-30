namespace Skyline.DataMiner.Github.Repositories.Presenters
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Remoting.Contexts;
	using System.Text;
	using System.Threading.Tasks;

	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Github.Repositories.Components;
	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Github.Repositories.Views;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class UsersPresenter
	{
		private readonly GithubModel model;
		private readonly UsersView usersView;

		public UsersPresenter(ScriptContext context, MainView mainView, UsersView usersView, GithubModel model)
		{
			this.model = model;
			this.usersView = usersView;

			usersView.Back.Pressed += (sender, e) => context.Controller.ShowDialog(mainView);
		}

		public void Load(string organizationId)
		{
			var users = model.GetUsersByOrganization(organizationId);
			usersView.Users.Collaborators.Options.Clear();
			usersView.Users.Collaborators.Options.AddRange(users.Select(x => Option.Create(x.Name, x)));
		}
	}
}
