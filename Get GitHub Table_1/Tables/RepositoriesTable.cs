namespace Get_GitHub_Table_1.Tables
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	using Skyline.DataMiner.Analytics.GenericInterface;
	using Skyline.DataMiner.Net.Messages;

	public enum RepositoryType
	{
		[Description("Other")]
		Other = 0,

		[Description("Automation")]
		Automation = 1,

		[Description("Connector")]
		Connector = 2,
	}

	internal class RepositoriesTableRow : IGithubTableRow
	{
		public string Name { get; set; }

		public string FullName { get; set; }

		public bool Private { get; set; }

		public string Description { get; set; }

		public string Owner { get; set; }

		public bool Fork { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public DateTime PushedAt { get; set; }

		public long Size { get; set; }

		public int Stars { get; set; }

		public int Watcher { get; set; }

		public string Language { get; set; }

		public string DefaultBranch { get; set; }

		public RepositoryType Type { get; set; }

		public GQIRow ToGQIRow()
		{
			return new GQIRow(
				new[]
				{
					new GQICell { Value = FullName },
					new GQICell { Value = Name },
					new GQICell { Value = Private },
					new GQICell { Value = Description },
					new GQICell { Value = Owner },
					new GQICell { Value = Fork },
					new GQICell { Value = CreatedAt.ToUniversalTime() },
					new GQICell { Value = UpdatedAt.ToUniversalTime() },
					new GQICell { Value = PushedAt.ToUniversalTime() },
					new GQICell { Value = Convert.ToDouble(Size) },
					new GQICell { Value = Stars },
					new GQICell { Value = Watcher },
					new GQICell { Value = Language },
					new GQICell { Value = DefaultBranch },
					new GQICell { Value = (int)Type, DisplayValue = Type.FriendlyDescription() },
				});
		}
	}

	internal class RepositoriesTable : IGithubTable
	{
		private readonly GQIDMS dms;

		public RepositoriesTable(GQIDMS dms, int agentId, int elementId)
		{
			this.dms = dms;
			AgentID = agentId;
			ElementID = elementId;
		}

		public int AgentID { get; }

		public int ElementID { get; }

		public GithubTable Type { get; } = GithubTable.Repositories;

		public GQIColumn[] GetColumns()
		{
			return new GQIColumn[]
			{
				new GQIStringColumn("Full Name"),
				new GQIStringColumn("Name"),
				new GQIBooleanColumn("Private"),
				new GQIStringColumn("Description"),
				new GQIStringColumn("Owner"),
				new GQIBooleanColumn("Fork"),
				new GQIDateTimeColumn("CreatedAt"),
				new GQIDateTimeColumn("UpdatedAt"),
				new GQIDateTimeColumn("PushedAt"),
				new GQIDoubleColumn("Size"),
				new GQIIntColumn("Stars"),
				new GQIIntColumn("Watchers"),
				new GQIStringColumn("Language"),
				new GQIStringColumn("Default Branch"),
				new GQIIntColumn("Type"),
			};
		}

		public GQIPage GetNextPage(GetNextPageInputArgs args)
		{
			var tableMessage = new GetPartialTableMessage(AgentID, ElementID, (int)Type, new string[0]);
			var response = (ParameterChangeEventMessage)dms.SendMessage(tableMessage);
			var fullname = response.NewValue.ArrayValue[0].ArrayValue;
			var name = response.NewValue.ArrayValue[1].ArrayValue;
			var @private = response.NewValue.ArrayValue[2].ArrayValue;
			var description = response.NewValue.ArrayValue[3].ArrayValue;
			var owner = response.NewValue.ArrayValue[4].ArrayValue;
			var fork = response.NewValue.ArrayValue[5].ArrayValue;
			var createdAt = response.NewValue.ArrayValue[6].ArrayValue;
			var updatedAt = response.NewValue.ArrayValue[7].ArrayValue;
			var pushedAt = response.NewValue.ArrayValue[8].ArrayValue;
			var size = response.NewValue.ArrayValue[9].ArrayValue;
			var stars = response.NewValue.ArrayValue[10].ArrayValue;
			var watchers = response.NewValue.ArrayValue[11].ArrayValue;
			var language = response.NewValue.ArrayValue[12].ArrayValue;
			var defaultBranch = response.NewValue.ArrayValue[13].ArrayValue;
			var type = response.NewValue.ArrayValue[14].ArrayValue;
			var rows = new List<RepositoriesTableRow>();
			for (int i = 0; i < fullname.Length; i++)
			{
				rows.Add(new RepositoriesTableRow
				{
					FullName = fullname[i].CellValue.StringValue,
					Name = name[i].CellValue.StringValue,
					Private = Convert.ToBoolean(@private[i].CellValue.DoubleValue),
					Description = description[i].CellValue.StringValue,
					Owner = owner[i].CellValue.StringValue,
					Fork = Convert.ToBoolean(fork[i].CellValue.DoubleValue),
					CreatedAt = DateTime.FromOADate(createdAt[i].CellValue.DoubleValue),
					UpdatedAt = DateTime.FromOADate(updatedAt[i].CellValue.DoubleValue),
					PushedAt = DateTime.FromOADate(pushedAt[i].CellValue.DoubleValue),
					Size = Convert.ToInt64(size[i].CellValue.DoubleValue),
					Stars = stars[i].CellValue.Int32Value,
					Watcher = watchers[i].CellValue.Int32Value,
					Language = language[i].CellValue.StringValue,
					DefaultBranch = defaultBranch[i].CellValue.StringValue,
					Type = (RepositoryType)type[i].CellValue.Int32Value,
				});
			}

			return new GQIPage(rows.Select(row => row.ToGQIRow()).ToArray())
			{
				HasNextPage = false,
			};
		}
	}
}
