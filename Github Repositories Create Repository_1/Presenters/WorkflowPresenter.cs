// Ignore Spelling: Github Nuget Api

namespace Skyline.DataMiner.Github.Repositories.Presenters
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Github.Repositories.Helpers;
	using Skyline.DataMiner.Github.Repositories.Models;
	using Skyline.DataMiner.Github.Repositories.Views;
	using Skyline.DataMiner.Github.Repositories.Views.Explanations;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class WorkflowPresenter
	{
		private readonly ScriptContext context;
		private readonly WorkflowView workflowView;
		private readonly GithubModel model;

		private readonly ExplanationInputView SonarCloudProjectIdView;
		private readonly ExplanationInputView SonarCloudTokenView;
		private readonly ExplanationInputView DataMinerTokenView;
		private readonly ExplanationInputView GithubTokenView;
		private readonly ExplanationInputView NugetApiTokenView;

		private readonly ProgressPresenter progressPresenter;
		private readonly ProgressView progressView;

		private RepositoryContext repositoryContext;
		private List<ExplanationInputView> requiredInputs = new List<ExplanationInputView>();

		public WorkflowPresenter(ScriptContext context, WorkflowView workflowView, GithubModel model)
		{
			this.context = context;
			this.workflowView = workflowView;
			this.model = model;

			SonarCloudProjectIdView = new SonarCloudProjectIdView(context.Engine);
			SonarCloudProjectIdView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			SonarCloudProjectIdView.BackButton.Pressed += ClearStatus;
			workflowView.SonarCloudProjectIdButton.Pressed += (sender, e) => context.Controller.ShowDialog(SonarCloudProjectIdView);

			SonarCloudTokenView = new SonarCloudTokenView(context.Engine);
			SonarCloudTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			SonarCloudTokenView.BackButton.Pressed += ClearStatus;
			workflowView.SonarCloudTokenButton.Pressed += (sender, e) => context.Controller.ShowDialog(SonarCloudTokenView);

			DataMinerTokenView = new DataMinerTokenView(context.Engine);
			DataMinerTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			DataMinerTokenView.BackButton.Pressed += ClearStatus;
			workflowView.DataMinerToken.Pressed += (sender, e) => context.Controller.ShowDialog(DataMinerTokenView);

			GithubTokenView = new GithubTokenView(context.Engine);
			GithubTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			GithubTokenView.BackButton.Pressed += ClearStatus;
			workflowView.GithubToken.Pressed += (sender, e) => context.Controller.ShowDialog(GithubTokenView);

			NugetApiTokenView = new NugetApiTokenView(context.Engine);
			NugetApiTokenView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			NugetApiTokenView.BackButton.Pressed += ClearStatus;
			workflowView.NugetApiToken.Pressed += (sender, e) => context.Controller.ShowDialog(NugetApiTokenView);

			progressView = new ProgressView(context.Engine);
			progressView.BackButton.Pressed += (sender, e) => context.Controller.ShowDialog(workflowView);
			progressPresenter = new ProgressPresenter(context, progressView, model);

			workflowView.NextButton.Pressed += NextButton_Pressed;
		}

		public void Load(RepositoryContext repositoryContext)
		{
			this.repositoryContext = repositoryContext;
			Invisible();
			switch (repositoryContext.WorkflowType)
			{
				case WorkflowType.None:
					workflowView.Status.Text = "Nothing to do here, you can go to the next page.";
					break;

				case WorkflowType.AutomationScript:
				case WorkflowType.Connector:
					//workflowView.SonarCloudProjectIdButton.IsVisible = true;
					SonarCloudProjectIdView.Input.Text = "Generate";
					workflowView.DataMinerToken.IsVisible = true;
					requiredInputs = new List<ExplanationInputView>
					{
						SonarCloudProjectIdView,
						DataMinerTokenView,
					};
					if (!repositoryContext.Public)
					{
						// workflowView.SonarCloudTokenButton.IsVisible = true;
						SonarCloudTokenView.Input.Text = "Generate";
						requiredInputs.Add(SonarCloudTokenView);
					}

					break;

				case WorkflowType.InternalNuget:
					//workflowView.SonarCloudProjectIdButton.IsVisible = true;
					SonarCloudProjectIdView.Input.Text = "Generate";
					workflowView.GithubToken.IsVisible = true;
					requiredInputs = new List<ExplanationInputView>
					{
						SonarCloudProjectIdView,
						GithubTokenView,
					};
					break;

				case WorkflowType.Nuget:
					//workflowView.SonarCloudProjectIdButton.IsVisible = true;
					SonarCloudProjectIdView.Input.Text = "Generate";
					workflowView.NugetApiToken.IsVisible = true;
					requiredInputs = new List<ExplanationInputView>
					{
						SonarCloudProjectIdView,
						NugetApiTokenView,
					};
					break;

				default:
					throw new NotSupportedException($"This repository type is not supported. [{repositoryContext.WorkflowType}]");
			}
		}

		private void Invisible()
		{
			workflowView.SonarCloudProjectIdButton.IsVisible = false;
			workflowView.SonarCloudTokenButton.IsVisible = false;
			workflowView.DataMinerToken.IsVisible = false;
			workflowView.GithubToken.IsVisible = false;
			workflowView.NugetApiToken.IsVisible = false;
		}

		private bool Validate(out string errors)
		{
			var result = true;
			var sb = new StringBuilder();
			foreach (var input in requiredInputs)
			{
				if (String.IsNullOrEmpty(input.Input.Text))
				{
					sb.AppendLine($"'{input.Title}' cannot be left empty.");
					result = false;
				}
			}

			errors = sb.ToString();
			return result;
		}

		private void ClearStatus(object sender, EventArgs e)
		{
			workflowView.Status.Text = String.Empty;
		}

		private void NextButton_Pressed(object sender, EventArgs e)
		{
			if(!Validate(out var errors))
			{
				workflowView.Status.Text += errors;
				return;
			}

			repositoryContext.SonarCloudProjectID = SonarCloudProjectIdView.Input.Text;
			repositoryContext.SonarCloudToken = SonarCloudTokenView.Input.Text;
			repositoryContext.DataMinerDeployKey = DataMinerTokenView.Input.Text;
			repositoryContext.GithubToken = GithubTokenView.Input.Text;
			repositoryContext.NugetApiKey = NugetApiTokenView.Input.Text;
			progressPresenter.Load(repositoryContext);
			context.Controller.ShowDialog(progressView);
		}
	}
}
