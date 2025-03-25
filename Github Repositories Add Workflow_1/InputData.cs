// Ignore Spelling: Github Workflow

namespace Github_Repositories_Add_Workflow_1
{
	using System;
	using System.Collections.Generic;

	using Newtonsoft.Json;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.SecureCoding.SecureSerialization.Json.Newtonsoft;

	public class InputData
	{
		public InputData(IEngine engine)
		{
			var dataMinerId = engine.GetScriptParam(10).Value;
			var elementId = engine.GetScriptParam(11).Value;
			var repositoryId = engine.GetScriptParam(12).Value;

			DataMinerID = Convert.ToInt32(dataMinerId);
			ElementId = Convert.ToInt32(elementId);
			RepositoryId = SecureNewtonsoftDeserialization.DeserializeObject<List<string>>(repositoryId)[0];
		}

		public int DataMinerID { get; }

		public int ElementId { get; }

		public string RepositoryId { get; }
	}
}
