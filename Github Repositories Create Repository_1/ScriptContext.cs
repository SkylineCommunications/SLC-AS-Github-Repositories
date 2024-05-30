namespace Skyline.DataMiner.Github.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ScriptContext
	{
		public ScriptContext(IEngine engine)
		{
			Engine = engine;
			Controller = new InteractiveController(Engine);

			AgentId = Convert.ToInt32(GetScriptParam("Agent Id").Single());
			ElementId = Convert.ToInt32(GetScriptParam("Element Id").Single());
			OrganizationId = GetScriptParam("Organization Id").Single();
		}

		public IEngine Engine { get; }

		public InteractiveController Controller { get; }

		public int AgentId { get; }

		public int ElementId { get; }

		public string OrganizationId { get; }

		private string[] GetScriptParam(string name)
		{
			var rawValue = Engine.GetScriptParam(name).Value;
			if (String.IsNullOrEmpty(rawValue))
			{
				throw new ArgumentException($"Script Param '{name}' cannot be left empty.");
			}

			if (rawValue.IsJsonArray())
			{
				return JsonConvert.DeserializeObject<string[]>(rawValue);
			}
			else
			{
				return new[] { rawValue };
			}
		}
	}
}
