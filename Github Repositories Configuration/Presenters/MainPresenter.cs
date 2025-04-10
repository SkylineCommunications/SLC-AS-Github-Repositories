// Ignore Spelling: Github

namespace GithubRepositoriesConfiguration.Presenters
{
	using System;

	using GithubRepositoriesConfiguration.Models;
	using GithubRepositoriesConfiguration.Views;
	using GithubRepositoriesConfiguration.Views.Settings;

	internal class MainPresenter
	{
		private readonly SonarCloudTokenView sonarCloudTokenView;

		public MainPresenter(ScriptContext context, MainView mainView)
		{
			sonarCloudTokenView = new SonarCloudTokenView(context.Engine);
			sonarCloudTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(mainView);

			mainView.SonarCloudToken.Pressed += (sender, e) => context.Controller.ShowDialog(sonarCloudTokenView);
			mainView.SaveButton.Pressed += SaveButton_Pressed;
			mainView.QuitButton.Pressed += (sender, e) => mainView.Engine.ExitSuccess("User aborted script.");
		}

		public void Load()
		{
			sonarCloudTokenView.Input.Text = GithubSettings.ReadSonarCloudToken();
		}

		private void SaveButton_Pressed(object sender, EventArgs e)
		{
			GithubSettings.WriteSonarCloudToken(sonarCloudTokenView.Input.Text);
		}
	}
}
