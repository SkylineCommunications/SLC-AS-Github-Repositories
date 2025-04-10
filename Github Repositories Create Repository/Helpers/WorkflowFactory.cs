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

				default:
					throw new NotSupportedException("This workflow type is not supported yet.");
			}
		}
	}
}
