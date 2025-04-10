namespace Install_1.LCA
{
	using System;
	using System.IO;
	using System.Linq;

	using Newtonsoft.Json.Linq;

	using Skyline.AppInstaller;
	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.AppPackages;
	using Skyline.DataMiner.Net.Helper;

	public class LcaInstaller
	{
		private const string ApplicationsDirectory = @"C:\Skyline DataMiner\applications";
		private const string LCA_FOLDERPATH = @"CompanionFiles\LCA";

		private AppInstaller installer;
		private readonly AppInstallContext context;
		private readonly Action<string> logMethod;

		private readonly string originalContentPath;

		public LcaInstaller(IConnection connection, AppInstallContext context, Action<string> logMethod)
		{
			originalContentPath = context.AppContentPath;

			this.context = context;
			this.context.AppContentPath = context.AppContentPath + LCA_FOLDERPATH;
			this.logMethod = logMethod;
			this.installer = new AppInstaller(connection, this.context);
		}

		public void InstallDefaultContent(int agentId, int elementId)
		{
			installer.InstallAppPackages();
			this.context.AppContentPath = originalContentPath;

			var files = Directory.GetFiles(context.AppContentPath + LCA_FOLDERPATH + @"\AppPackages", "*.dmapp", SearchOption.AllDirectories);
			files.ForEach((file) => SetElementInfo(Path.GetFileNameWithoutExtension(file), agentId, elementId));
		}

		private void SetElementInfo(string appId, int agentId, int elementId)
		{
			try
			{
				// Get public version
				var info = System.IO.File.ReadAllText(Path.Combine(ApplicationsDirectory, appId, "App.info.json"));
				var publicVersion = JObject.Parse(info)["PublicVersion"].Value<int>();
				logMethod($"Public Version: {publicVersion}");
				logMethod($"Agent ID: {agentId}");
				logMethod($"Element ID: {elementId}");

				// set name
				var versionFolder = Path.Combine(ApplicationsDirectory, appId, $"version_{publicVersion}");
				var files = Directory.GetFiles(versionFolder, "*.json", SearchOption.AllDirectories);
				foreach (var file in files)
				{
					var content = System.IO.File.ReadAllText(file);
					content = content.Replace("REPLACE_DATAMINER_ID_REPLACE", Convert.ToString(agentId));
					content = content.Replace("REPLACE_ELEMENT_ID_REPLACE", Convert.ToString(elementId));
					System.IO.File.WriteAllText(file, content);
				}
			}
			catch (Exception)
			{
				logMethod($"Could not set agent/element ID of app with ID: {appId}");
			}
		}
	}
}
