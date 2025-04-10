namespace Install_1.Element
{
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public class ElementInstaller
	{
		private IEngine engine;
		private DmsElementId elementId;
		private string protocolVersion;

		public ElementInstaller(IEngine engine, string protocolVersion)
		{
			this.engine = engine;
			this.protocolVersion = protocolVersion;
		}

		public DmsElementId InstallDefaultContent()
		{
			var dms = engine.GetDms();

			// Check if example element exists
			if (dms.ElementExists("Github Repositories Example"))
			{
				var element = dms.GetElement("Github Repositories Example");
				element.AssignProtocolVersion(engine, protocolVersion);
				return element.DmsElementId;
			}

			// Create it
			elementId = CreateElement();

			System.Threading.Thread.Sleep(15000);

			AddRepository("SkylineCommunications", "SLC-C-Github-Repositories");
			AddRepository("SkylineCommunications", "Low-Code-App-Editor");
			engine.FindElement("Github Repositories Example").Restart();
			return elementId;
		}

		public DmsElementId CreateElement()
		{
			var dms = engine.GetDms();
			var agent = dms.GetAgents().First();

			var protocol = dms.GetProtocol("Github Repositories", protocolVersion);
			var name = "Github Repositories Example";

			var port = new Tcp("https://api.github.com", 443);
			var httpConnection = new HttpConnection(port);

			var configuration = new ElementConfiguration(dms, name, protocol, new List<IElementConnection> { httpConnection });

			var createdElementId = agent.CreateElement(configuration);
			return createdElementId;
		}

		public void AddRepository(string owner, string name)
		{
			var element = engine.FindElement("Github Repositories Example");
			element.SetParameter(501, name);
			element.SetParameter(502, owner);
			element.SetParameter(500, 1);
		}
	}
}
