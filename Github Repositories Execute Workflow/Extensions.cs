// Ignore Spelling: Github

namespace GithubRepositoriesExecuteWorkflow
{
	using System;

	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	public static class Extensions
	{
		public static bool TryGetRow(this IDmsTable table, string rowKey, out object[] row)
		{
			try
			{
				row = table.GetRow(rowKey);
				return true;
			}
			catch (Exception)
			{
				row = null;
				return false;
			}
		}
	}
}
