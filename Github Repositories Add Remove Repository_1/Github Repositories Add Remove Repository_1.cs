/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
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

dd/mm/2023	1.0.0.1		XXX, Skyline	Initial version
****************************************************************************
*/

namespace Github_Repositories_Add_Remove_Repository_1
{
    using System;

    using Newtonsoft.Json;

    using Skyline.DataMiner.Automation;
    using Skyline.Protocol.Tables;

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
            var agentId = Convert.ToInt32(engine.GetScriptParam("Agent Id").Value);
            var elementId = Convert.ToInt32(engine.GetScriptParam("Element Id").Value);
            var name = engine.GetScriptParam("Repo Name").Value.Replace("[", String.Empty).Replace("]", String.Empty).Replace("\"", String.Empty);
            var owner = engine.GetScriptParam("Repo Owner").Value.Replace("[", String.Empty).Replace("]", String.Empty).Replace("\"", String.Empty);
            var remove = engine.GetScriptParam("Add or Remove").Value;

            var element = engine.FindElement(agentId, elementId);

            if (remove.ToUpper() == "ADD")
            {
                AddRepo(element, owner, name);
            }

            if (remove.ToUpper() == "REMOVE")
            {
                RemoveRepo(element, owner, name);
            }

            Refresh(element);
        }

        private static void AddRepo(Element element, string owner, string name)
        {
            var request = new AddRepositoriesTableRequest(owner, name);
            element.SetParameter(992, JsonConvert.SerializeObject(request));
        }

        private static void RemoveRepo(Element element, string owner, string name)
        {
            var request = new RemoveRepositoriesTableRequest($"{owner}/{name}");
            element.SetParameter(992, JsonConvert.SerializeObject(request));
        }

        private static void Refresh(Element element)
        {
            element.SetParameterByPrimaryKey(21006, "201", 1);
            element.SetParameterByPrimaryKey(21006, "202", 1);
            element.SetParameterByPrimaryKey(21006, "203", 1);
            element.SetParameterByPrimaryKey(21006, "204", 1);
        }
    }
}