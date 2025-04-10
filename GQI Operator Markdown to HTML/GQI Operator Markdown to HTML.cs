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

10/04/2025	1.0.0.1		AMA, Skyline	Initial version
****************************************************************************
*/

namespace GQIOperatorMarkdownToHTML
{
	using Markdig;

	using Skyline.DataMiner.Analytics.GenericInterface;

	[GQIMetaData(Name = "Markdown To HTML")]
	public class MarkdownToHtmlOperator : IGQIColumnOperator, IGQIRowOperator, IGQIInputArguments
	{
		private readonly GQIColumnDropdownArgument _markdownColumnArg = new GQIColumnDropdownArgument("Markdown Column")
		{
			IsRequired = true,
			Types = new[] { GQIColumnType.String },
		};

		private readonly GQIStringArgument _resultColumnNameArg = new GQIStringArgument("Result Column Name")
		{
			IsRequired = false,
			DefaultValue = "Rendered HTML",
		};

		private GQIColumn _markdownColumn;
		private GQIStringColumn _resultColumn = new GQIStringColumn("Rendered HTML");

		public GQIArgument[] GetInputArguments()
		{
			return new GQIArgument[]
			{
				_markdownColumnArg,
				_resultColumnNameArg,
			};
		}

		public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
		{
			_markdownColumn = args.GetArgumentValue(_markdownColumnArg);
			_resultColumn = new GQIStringColumn(args.GetArgumentValue(_resultColumnNameArg));
			return new OnArgumentsProcessedOutputArgs();
		}

		public void HandleColumns(GQIEditableHeader header)
		{
			header.AddColumns(_resultColumn);
		}

		public void HandleRow(GQIEditableRow row)
		{
			var markdown = row.GetValue<string>(_markdownColumn);
			var html = Markdown.ToHtml(markdown);

			row.SetValue(_resultColumn, html);
		}
	}
}
