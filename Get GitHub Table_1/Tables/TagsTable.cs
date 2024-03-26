// Ignore Spelling: Sha

namespace Get_GitHub_Table_1.Tables
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Analytics.GenericInterface;
	using Skyline.DataMiner.Net.Messages;

	internal class TagsTableRow : IGithubTableRow
	{
		public string ID { get; set; }

		public string Name { get; set; }

		public string RepositoryID { get; set; }

		public string CommitSha { get; set; }

		public GQIRow ToGQIRow()
		{
			return new GQIRow(
				new[]
				{
					new GQICell { Value = ID },
					new GQICell { Value = Name },
					new GQICell { Value = RepositoryID },
					new GQICell { Value = CommitSha },
				});
		}
	}

	internal class TagsTable : IGithubTable
	{
		private readonly GQIDMS dms;

		public TagsTable(GQIDMS dms, int agentId, int elementId)
		{
			this.dms = dms;
			AgentID = agentId;
			ElementID = elementId;
		}

		public int AgentID { get; }

		public int ElementID { get; }

		public GithubTable Type { get; } = GithubTable.Tags;

		public GQIColumn[] GetColumns()
		{
			return new[]
			{
				new GQIStringColumn("ID"),
				new GQIStringColumn("Name"),
				new GQIStringColumn("Repository ID"),
				new GQIStringColumn("Commit SHA"),
			};
		}

		public GQIPage GetNextPage(GetNextPageInputArgs args)
		{
			var tableMessage = new GetPartialTableMessage(AgentID, ElementID, (int)Type, new string[0]);
			var response = (ParameterChangeEventMessage)dms.SendMessage(tableMessage);
			var id = response.NewValue.ArrayValue[0].ArrayValue;
			var name = response.NewValue.ArrayValue[1].ArrayValue;
			var repositoryId = response.NewValue.ArrayValue[2].ArrayValue;
			var commitsha = response.NewValue.ArrayValue[3].ArrayValue;
			var rows = new List<TagsTableRow>();
			for (int i = 0; i < id.Length; i++)
			{
				rows.Add(new TagsTableRow
				{
					ID = id[i].CellValue.StringValue,
					Name = name[i].CellValue.StringValue,
					RepositoryID = repositoryId[i].CellValue.StringValue,
					CommitSha = commitsha[i].CellValue.StringValue,
				});
			}

			return new GQIPage(rows.Select(row => row.ToGQIRow()).ToArray())
			{
				HasNextPage = false,
			};
		}
	}
}
