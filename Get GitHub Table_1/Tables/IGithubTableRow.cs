namespace Get_GitHub_Table_1.Tables
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Analytics.GenericInterface;

	internal interface IGithubTableRow
	{
		GQIRow ToGQIRow();
	}
}
