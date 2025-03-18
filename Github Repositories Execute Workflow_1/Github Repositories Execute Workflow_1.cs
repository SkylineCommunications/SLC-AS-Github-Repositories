/*
****************************************************************************
*  Copyright (c) 2025,  Skyline Communications NV  All Rights Reserved.    *
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

dd/mm/2025	1.0.0.1		XXX, Skyline	Initial version
****************************************************************************
*/

namespace Github_Repositories_Execute_Workflow_1
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Common;
    using Common.DomIds;
    using Github_Repositories_Extensions;
    using Github_Repositories_InputData;
    using Newtonsoft.Json;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.ConnectorAPI.Github.Repositories;
    using Skyline.DataMiner.ConnectorAPI.Github.Repositories.InterAppMessages.Workflows;
    using Skyline.DataMiner.Core.DataMinerSystem.Automation;
    using Skyline.DataMiner.Core.DataMinerSystem.Common;
    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;

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
            try
            {
                RunSafe(engine);
            }
            catch (ScriptAbortException)
            {
                // Catch normal abort exceptions (engine.ExitFail or engine.ExitSuccess)
                throw; // Comment if it should be treated as a normal exit of the script.
            }
            catch (ScriptForceAbortException)
            {
                // Catch forced abort exceptions, caused via external maintenance messages.
                throw;
            }
            catch (ScriptTimeoutException)
            {
                // Catch timeout exceptions for when a script has been running for too long.
                throw;
            }
            catch (InteractiveUserDetachedException)
            {
                // Catch a user detaching from the interactive script by closing the window.
                // Only applicable for interactive scripts, can be removed for non-interactive scripts.
                throw;
            }
            catch (Exception e)
            {
                engine.ExitFail("Run|Something went wrong: " + e);
            }
        }

        private void RunSafe(IEngine engine)
        {
            var input = new InputData(engine);
            var workflows = engine.GetDms().GetElement(new DmsElementId(input.DataMinerID, input.ElementId)).GetTable(1600);
            if (!workflows.TryGetRow(input.RowKey, out var workflowRow))
            {
                throw new KeyNotFoundInTableException("Could not retrieve the selected workflow. Please check it is available on the element.");
            }

            // if (workflow.Length < 17 || Convert.ToString(workflow[15]) == "-2" || Convert.ToString(workflow[16]) == "-2")
            // {
            //     throw new AccessViolationException("The public key is not available for this repository. You can try to manually poll the repository's public keys by pressing the refresh button on the 'Poll Manager' page. If that doesn't work check if the api token has access to the repository?");
            // }

            var repoIdInfo = Convert.ToString(workflowRow[0]).Split('/');
            var owner = repoIdInfo[0];
            var repoName = repoIdInfo[1];
            var workflowId = repoIdInfo[repoIdInfo.Length - 1];

            var element = new GithubRepositories(engine.GetUserConnection(), input.DataMinerID, input.ElementId);
            var value = engine.GetScriptParam("Inputs").Value;
            var inputs = String.IsNullOrWhiteSpace(value) || value == "none" ? new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(value);

            var executeWorkflowRequest = new ExecuteWorkflowRequest
            {
                RepositoryId = new RepositoryId(owner, repoName),
                WorkflowInputs = inputs,
                WorkflowId = workflowId,
            };

            var response = element.SendSingleResponseMessage(executeWorkflowRequest);
        }
    }
}