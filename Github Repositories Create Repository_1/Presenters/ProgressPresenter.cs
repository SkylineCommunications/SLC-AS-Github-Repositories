namespace Skyline.DataMiner.Github.Repositories.Presenters
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Skyline.DataMiner.Github.Repositories.Helpers;
	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Github.Repositories.Views;

	public class ProgressPresenter
	{
		private readonly ScriptContext context;
		private readonly ProgressView progressView;
		private readonly GithubModel model;

		private RepositoryContext repositoryContext;
		private bool createdRepository = false;

		public ProgressPresenter(ScriptContext context, ProgressView progressView, GithubModel model)
		{
			this.context = context;
			this.progressView = progressView;
			this.model = model;

			this.model.CreationProgress += Model_CreationProgress;
			this.progressView.CreateButton.Pressed += CreateButton_Pressed;
			this.progressView.CreateButton.Pressed += FinishButton_Pressed;
		}

		public void Load(RepositoryContext context)
		{
			this.repositoryContext = context;
			this.createdRepository = false;
			this.progressView.Status.Text = "Ready to create the repository. Press create to start the creation process.";
			this.progressView.CreateButton.Text = "Create";
			this.progressView.CreateButton.IsEnabled = true;
			this.progressView.ShowInteractive();
		}

		private void CreateButton_Pressed(object sender, EventArgs e)
		{
			if (createdRepository)
			{
				return;
			}

			progressView.ShowStatic(true);
			progressView.Status.Text = String.Empty;
			progressView.CreateButton.IsEnabled = false;

			if (model.CreateRepository(repositoryContext))
			{
				progressView.CreateButton.Text = "Finish";
				progressView.CreateButton.IsEnabled = true;
				createdRepository = true;
			}

			progressView.ShowStatic(false);
			progressView.ShowInteractive();
		}

		private void FinishButton_Pressed(object sender, EventArgs e)
		{
			if (createdRepository)
			{
				context.Engine.ExitSuccess("Successfully created a repository.");
			}
		}

		private void Model_CreationProgress(object sender, Helpers.StatusProgressEventArgs e)
		{
			progressView.ShowStatic(true);
			progressView.Status.Text += $"\n{e.ProgressMessage}";
		}
	}
}
