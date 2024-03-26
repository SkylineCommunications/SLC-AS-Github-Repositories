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

namespace Get_GitHub_Table_1
{
	using System;
	using System.Linq;
	using System.Reflection;

	using Get_GitHub_Table_1.Tables;

	using Skyline.DataMiner.Analytics.GenericInterface;

	[GQIMetaData(Name = "Get GitHub Table")]
	public class GetGitHubTable : IGQIDataSource, IGQIInputArguments, IGQIOnInit
	{
		private readonly GQIStringArgument elementArgs = new GQIStringArgument("Element ID");
		private readonly GQIStringDropdownArgument tableArgs = new GQIStringDropdownArgument("Table", Enum.GetValues(typeof(GithubTable))
			.Cast<GithubTable>()
			.Select(x => x.FriendlyDescription())
			.ToArray())
		{
			IsRequired = true,
		};

		private IGithubTable table;

		private GQIDMS dms;

		#region Arguments
		public GQIArgument[] GetInputArguments()
		{
			return new GQIArgument[] { elementArgs, tableArgs };
		}

		public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
		{
			var tableType = Extensions.ParseEnumDescription<GithubTable>(args.GetArgumentValue(tableArgs));
			var dmselementid = args.GetArgumentValue(elementArgs).Split('/');
			var agentId = Convert.ToInt32(dmselementid[0]);
			var elementId = Convert.ToInt32(dmselementid[1]);

			table = TableFactory.GetTable(dms, tableType, agentId, elementId);

			return new OnArgumentsProcessedOutputArgs();
		}
		#endregion

		#region DataSource
		public GQIColumn[] GetColumns()
		{
			return table.GetColumns();
		}

		public GQIPage GetNextPage(GetNextPageInputArgs args)
		{
			return table.GetNextPage(args);
		}
		#endregion

		public OnInitOutputArgs OnInit(OnInitInputArgs args)
		{
			dms = args.DMS;
			return new OnInitOutputArgs();
		}
	}
}