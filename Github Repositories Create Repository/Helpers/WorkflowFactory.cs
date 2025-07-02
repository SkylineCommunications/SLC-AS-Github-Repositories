// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System;

	using Skyline.DataMiner.ConnectorAPI.Github.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows.Data;

	public static class WorkflowFactory
	{
		public static IGithubRequest Create(RepositoryContext context)
		{
			switch (context.WorkflowType)
			{
				case WorkflowType.AutomationScriptCI:
					return new AddAutomationScriptCIWorkflowRequest
					{
						RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
						Data = new AutomationScriptCIWorkflowData
						{
							DataMinerKey = context.DataMinerDeployKey,
							SonarCloudProjectID = context.SonarCloudProjectID,
							SonarToken = context.Public ? string.Empty : context.SonarCloudToken,
						},
					};

				case WorkflowType.AutomationScriptCICD:
					return new AddAutomationScriptCICDWorkflowRequest
					{
						RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
						Data = new AutomationScriptCICDWorkflowData
						{
							DataMinerKey = context.DataMinerDeployKey,
							SonarCloudProjectID = context.SonarCloudProjectID,
							SonarToken = context.Public ? string.Empty : context.SonarCloudToken,
						},
					};

				case WorkflowType.ConnectorCI:
					return new AddConnectorCIWorkflowRequest
					{
						RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
						Data = new ConnectorCIWorkflowData
						{
							DataMinerKey = context.DataMinerDeployKey,
							SonarCloudProjectID = context.SonarCloudProjectID,
							SonarToken = context.Public ? string.Empty : context.SonarCloudToken,
						},
					};

				case WorkflowType.NugetSolutionCICD:
					return new AddNugetCICDWorkflowRequest
					{
						RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
						Data = new NugetCICDWorkflowData
						{
							NugetApiKey = context.NugetApiKey,
							SonarCloudProjectID = context.SonarCloudProjectID,
						},
					};

				case WorkflowType.InternalNugetSolutionCICD:
					return new AddInternalNugetCICDWorkflowRequest
					{
						RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
						Data = new InternalNugetCICDWorkflowData
						{
							GithubNugetApiKey = context.GithubToken,
							SonarCloudProjectID = context.SonarCloudProjectID,
							SonarToken = context.Public ? string.Empty : context.SonarCloudToken,
						},
					};

				case WorkflowType.AppPackage:
					var request = new AddCustomWorkflowRequest
					{
						RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
						Workflow = new CustomWorkflowData
						{
							WorkflowName = "DataMiner App Pacakge",
							Variables = new System.Collections.Generic.Dictionary<string, string>
							{
								{ "SONAR_NAME", context.SonarCloudProjectID },
							},
							Secrets = new System.Collections.Generic.Dictionary<string, string>(),
							WorkflowYaml = @"name: DataMiner App Package

# Controls when the workflow will run
on:
  # Triggers the workflow on push or pull request events but only for the master branch
  push:
    branches: []
    tags:
      - ""[0-9]+.[0-9]+.[0-9]+.[0-9]+""
      - ""[0-9]+.[0-9]+.[0-9]+.[0-9]+-**""
      - ""[0-9]+.[0-9]+.[0-9]+""
      - ""[0-9]+.[0-9]+.[0-9]+-**""

  # Allows you to run this workflow manually from the Actions tab
  workflow_dispatch:

# A workflow run is made up of one or more jobs that can run sequentially or in parallel
jobs:

  CI:
    uses: SkylineCommunications/_ReusableWorkflows/.github/workflows/DataMiner App Packages Master Workflow.yml@main
    with:
      configuration: Release
      sonarCloudProjectName: ${{ vars.SONAR_NAME }}
      # solutionFilterName: ""MySolutionFilter.slnf""
    # secrets:
      # sonarCloudToken: ${{ secrets.SONAR_TOKEN }}
      # dataminerToken: ${{ secrets.DATAMINER_TOKEN }}
      # overrideCatalogDownloadToken: ${{ secrets.OVERRIDE_DATAMINER_TOKEN }}",
						},
					};

					if (context.Organization != "SkylineCommunications" ||
						!String.IsNullOrEmpty(context.DataMinerToken))
					{
						request.Workflow.WorkflowYaml = request.Workflow.WorkflowYaml.Replace(
							"# dataminerToken: ${{ secrets.DATAMINER_TOKEN }}",
							"dataminerToken: ${{ secrets.DATAMINER_TOKEN }}");
						request.Workflow.Secrets.Add("DATAMINER_TOKEN", context.DataMinerToken);
					}

					if (context.Organization != "SkylineCommunications" ||
						!String.IsNullOrEmpty(context.SonarCloudToken))
					{
						request.Workflow.WorkflowYaml = request.Workflow.WorkflowYaml.Replace(
							"# sonarCloudToken: ${{ secrets.SONAR_TOKEN }}",
							"sonarCloudToken: ${{ secrets.SONAR_TOKEN }}");
						request.Workflow.Secrets.Add("SONAR_TOKEN", context.SonarCloudToken);
					}

					return request;

				default:
					throw new NotSupportedException("This workflow type is not supported yet.");
			}
		}
	}
}
