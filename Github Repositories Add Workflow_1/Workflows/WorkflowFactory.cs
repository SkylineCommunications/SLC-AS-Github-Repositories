// Ignore Spelling: Workflow Github

namespace Github_Repositories_Add_Workflow_1
{
	using System;

	using Common;

	using Skyline.DataMiner.ConnectorAPI.Github.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows.Data;
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

	public static class WorkflowFactory
	{
		public static Message Create(AddWorkflowInstance instance)
		{
			switch (instance.Type)
			{
				case Common.DomIds.Github_Repositories.Enums.Workflowtype.AutomationScriptCI:
					return new AddAutomationScriptCIWorkflowRequest
					{
						RepositoryId = new RepositoryId(instance.RepositoryID.Split('/')[0], instance.RepositoryID.Split('/')[1]),
						Data = new AutomationScriptCIWorkflowData
						{
							DataMinerKey = instance.AutomationScriptCI_DataMinerDeployKey,
							SonarCloudProjectID = instance.AutomationScriptCI_SonarCloudProjectID,
							SonarToken = instance.AutomationScriptCI_IsPrivateRepository ? instance.PrivateSonarCloudToken : String.Empty,
						},
					};

				case Common.DomIds.Github_Repositories.Enums.Workflowtype.AutomationScriptCICD:
					return new AddAutomationScriptCICDWorkflowRequest
					{
						RepositoryId = new RepositoryId(instance.RepositoryID.Split('/')[0], instance.RepositoryID.Split('/')[1]),
						Data = new AutomationScriptCICDWorkflowData
						{
							DataMinerKey = instance.AutomationScriptCICD_DataMinerDeployKey,
							SonarCloudProjectID = instance.AutomationScriptCICD_SonarCloudProjectID,
							SonarToken = instance.AutomationScriptCICD_IsPrivateRepository ? instance.PrivateSonarCloudToken : String.Empty,
						},
					};

				case Common.DomIds.Github_Repositories.Enums.Workflowtype.ConnectorCI:
					return new AddConnectorCIWorkflowRequest
					{
						RepositoryId = new RepositoryId(instance.RepositoryID.Split('/')[0], instance.RepositoryID.Split('/')[1]),
						Data = new ConnectorCIWorkflowData
						{
							DataMinerKey = instance.ConnectorCI_DataMinerDeployKey,
							SonarCloudProjectID = instance.ConnectorCI_SonarCloudProjectID,
							SonarToken = instance.ConnectorCI_IsPrivateRepository ? instance.PrivateSonarCloudToken : String.Empty,
						},
					};

				case Common.DomIds.Github_Repositories.Enums.Workflowtype.NugetSolutionCICD:
					return new AddNugetCICDWorkflowRequest
					{
						RepositoryId = new RepositoryId(instance.RepositoryID.Split('/')[0], instance.RepositoryID.Split('/')[1]),
						Data = new NugetCICDWorkflowData
						{
							NugetApiKey = instance.NugetCICD_NugetApiKey,
							SonarCloudProjectID = instance.NugetCICD_SonarCloudProjectID,
						},
					};

				case Common.DomIds.Github_Repositories.Enums.Workflowtype.InternalNugetSolutionCICD:
					return new AddInternalNugetCICDWorkflowRequest
					{
						RepositoryId = new RepositoryId(instance.RepositoryID.Split('/')[0], instance.RepositoryID.Split('/')[1]),
						Data = new InternalNugetCICDWorkflowData
						{
							GithubNugetApiKey = instance.InternalNugetCICD_GithubToken,
							SonarCloudProjectID = instance.InternalNugetCICD_SonarCloudProjectID,
							SonarToken = instance.InternalNugetCICD_IsPrivateRepository ? instance.PrivateSonarCloudToken : String.Empty,
						},
					};

				default:
					throw new NotSupportedException("This workflow type is not supported yet.");
			}
		}
	}
}
