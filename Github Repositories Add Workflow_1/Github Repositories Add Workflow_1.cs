/*
****************************************************************************
*  Copyright (c) 2024,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

dd/mm/2024	1.0.0.1		XXX, Skyline	Initial version
****************************************************************************
*/

namespace Github_Repositories_Add_Workflow_1
{
	using Common;
	using Common.DomIds;
	using Common.States;

	using Newtonsoft.Json;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories;
	using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel.Actions;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			var helper = new DomHelper(engine.SendSLNetMessages, Github_Repositories.ModuleId);
			var instance = new AddWorkflowInstance(helper);
			var input = new InputData(engine);
			instance.DataMinerID = input.DataMinerID;
			instance.ElementID = input.ElementId;
			instance.RepositoryID = input.RepositoryId;
			instance.Type = Github_Repositories.Enums.Workflowtype.AutomationScriptCI;
			instance.Save(helper);

			engine.GenerateInformation(JsonConvert.SerializeObject(instance));
		}

		[AutomationEntryPoint(AutomationEntryPointType.Types.OnDomAction)]
		public void OnDomAction(IEngine engine, ExecuteScriptDomActionContext data)
		{
			// Helpers
			var instanceId = (DomInstanceId)data.ContextId;
			var helper = new DomHelper(engine.SendSLNetMessages, Github_Repositories.ModuleId);
			var instance = new AddWorkflowInstance(helper, instanceId);
			var newStatus = States.GetState(instance, data.ActionId);

			engine.GenerateInformation(data.ActionId);
			engine.GenerateInformation(instance.Status.Status.ToString());
			engine.GenerateInformation(JsonConvert.SerializeObject(instance));
			engine.GenerateInformation(newStatus.ToString());

			// Transition DOM Instance
			instance.Status.Transition(newStatus);
			engine.GenerateInformation("Executes transition.");
			engine.GenerateInformation(instance.Status.Status.ToString());
			instance.Save(helper);
			engine.GenerateInformation("Saved instance.");

			// If state is completed then we can add the workflow to the repository
			if (newStatus != Statuses.Result)
			{
				return;
			}

			var request = WorkflowFactory.Create(instance);
			var element = new GithubRepositories(engine.GetUserConnection(), instance.DataMinerID, instance.ElementID);
			var result = (AddWorkflowResponse)element.SendSingleResponseMessage(request);
			instance.ResultMessage = result.Description;
			instance.Save(helper);
			engine.GenerateInformation(result.Description);
		}
	}
}