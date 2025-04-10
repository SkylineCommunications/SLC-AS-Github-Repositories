namespace Install_1.Element
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.Messages;

	public static class ElementExtensions
	{
		public static void AssignProtocolVersion(this IDmsElement element, IEngine engine, string versionToSet)
		{
			AddElementMessage msg = new AddElementMessage
			{
				DataMinerID = element.AgentId,
				ElementName = element.Name,
				ElementID = element.Id,
				ProtocolVersion = versionToSet,
			};

			engine.SendSLNetMessage(msg);
		}
	}
}