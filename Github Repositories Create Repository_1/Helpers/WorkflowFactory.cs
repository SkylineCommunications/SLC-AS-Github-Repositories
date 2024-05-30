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
				case Repositories.WorkflowType.AutomationScript:
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

				//case Common.DomIds.Github_Repositories.Enums.Workflowtype.AutomationScriptCICD:
				//	return new AddAutomationScriptCICDWorkflowRequest
				//	{
				//		RepositoryId = new RepositoryId(instance.RepositoryID.Split('/')[0], instance.RepositoryID.Split('/')[1]),
				//		Data = new AutomationScriptCICDWorkflowData
				//		{
				//			DataMinerKey = instance.AutomationScriptCICD_DataMinerDeployKey,
				//			SonarCloudProjectID = instance.AutomationScriptCICD_SonarCloudProjectID,
				//			SonarToken = instance.AutomationScriptCICD_IsPrivateRepository ? instance.PrivateSonarCloudToken : String.Empty,
				//		},
				//	};

				case Repositories.WorkflowType.Connector:
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

				case Repositories.WorkflowType.Nuget:
					return new AddNugetCICDWorkflowRequest
					{
						RepositoryId = new RepositoryId(context.Id.Split('/')[0], context.Id.Split('/')[1]),
						Data = new NugetCICDWorkflowData
						{
							NugetApiKey = context.NugetApiKey,
							SonarCloudProjectID = context.SonarCloudProjectID,
						},
					};

				case Repositories.WorkflowType.InternalNuget:
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
