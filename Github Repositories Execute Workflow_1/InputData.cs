// Ignore Spelling: Github Workflow

namespace Github_Repositories_InputData
{
	using System;
	using System.Collections.Generic;

	using Newtonsoft.Json;

	using Skyline.DataMiner.Automation;

	public class InputData
	{
		public InputData(IEngine engine)
		{
			var dataMinerId = engine.GetScriptParam(10).Value;
			var elementId = engine.GetScriptParam(11).Value;
			var rowKey = engine.GetScriptParam(12).Value;

			DataMinerID = Convert.ToInt32(dataMinerId);
			ElementId = Convert.ToInt32(elementId);
			RowKey = Convert.ToString(rowKey);
		}

		public int DataMinerID { get; }

		public int ElementId { get; }

		public string RowKey { get; }
	}
}
