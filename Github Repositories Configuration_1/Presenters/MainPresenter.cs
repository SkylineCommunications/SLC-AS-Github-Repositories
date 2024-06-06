// Ignore Spelling: Github

namespace Github_Repositories_Configuration_1.Presenters
{
	using System;

	using Github_Repositories_Configuration_1.Models;
	using Github_Repositories_Configuration_1.Views;
	using Github_Repositories_Configuration_1.Views.Settings;

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
