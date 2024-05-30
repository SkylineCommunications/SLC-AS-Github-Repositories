// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System;

	public class StatusProgressEventArgs : EventArgs
	{
		public StatusProgressEventArgs(string progressMessage) => ProgressMessage = progressMessage;

		public string ProgressMessage { get; internal set; }
	}
}
