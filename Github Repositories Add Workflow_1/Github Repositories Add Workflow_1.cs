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
			instance.Save(helper);
		}

		[AutomationEntryPoint(AutomationEntryPointType.Types.OnDomAction)]
		public void OnDomAction(IEngine engine, ExecuteScriptDomActionContext data)
		{
			// Helpers
			var instanceId = (DomInstanceId)data.ContextId;
			var helper = new DomHelper(engine.SendSLNetMessages, Github_Repositories.ModuleId);
			var instance = new AddWorkflowInstance(helper, instanceId);
			var newStatus = States.GetState(instance, data.ActionId);

			// Transition DOM Instance
			instance.Status.Transition(newStatus);

			// If state is completed then we can add the workflow to the repository
			if (newStatus != Statuses.Completed)
			{
				return;
			}

			var request = WorkflowFactory.Create(instance);
			var element = engine.FindElement(instance.DataMinerID, instance.ElementID);
			element.SetParameter(1592, request);
		}
	}
}