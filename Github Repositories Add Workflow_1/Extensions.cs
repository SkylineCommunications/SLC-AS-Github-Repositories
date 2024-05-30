namespace Github_Repositories_Add_Workflow_1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

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
