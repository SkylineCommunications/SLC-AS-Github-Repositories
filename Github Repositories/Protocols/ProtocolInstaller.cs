namespace Install_1.Protocols
{
	using System;

	using Skyline.AppInstaller;
	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.AppPackages;

	public class ProtocolInstaller
	{
		private const string PROTOCOLS_FOLDERPATH = @"CompanionFiles\Protocols";

		private AppInstaller installer;
		private readonly AppInstallContext context;
		private readonly Action<string> logMethod;

		private readonly string originalContentPath;

		public ProtocolInstaller(IConnection connection, AppInstallContext context, Action<string> logMethod)
		{
			originalContentPath = context.AppContentPath;

			this.context = context;
			this.context.AppContentPath = context.AppContentPath + PROTOCOLS_FOLDERPATH;
			this.logMethod = logMethod;
			this.installer = new AppInstaller(connection, this.context);
		}

		public void InstallDefaultContent()
		{
			installer.InstallProtocols();
			this.context.AppContentPath = originalContentPath;
		}
	}
}
