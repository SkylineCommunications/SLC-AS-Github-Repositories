// Ignore Spelling: Github

namespace Github_Repositories_Configuration_1
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class ScriptContext
	{
		public ScriptContext(IEngine engine)
		{
			Engine = engine;
			Controller = new InteractiveController(Engine);
		}

		public IEngine Engine { get; }

		public InteractiveController Controller { get; }
	}
}
