// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories
{
	using System.ComponentModel;

	using Skyline.DataMiner.Github.Repositories.Attributes;

	public enum WorkflowType
	{
		None,
		[Description("Automation Script")]
		AutomationScript,
		Connector,
		[Description("Internal Nuget")]
		InternalNuget,
		Nuget,
	}

	public enum RepositoryType
	{
		[ShortDescription("C")]
		[Description("Connector")]
		Connector,

		[ShortDescription("V")]
		[Description("Visio")]
		Visio,

		[ShortDescription("S")]
		[Description("Solution")]
		Solution,

		[ShortDescription("F")]
		[Description("Function")]
		Function,

		[ShortDescription("AS")]
		[Description("Automation Script")]
		Automation_Script,

		[ShortDescription("D")]
		[Description("Dashboard")]
		Dashboard,

		[ShortDescription("PLS")]
		[Description("Profile-Load Script")]
		Profile_Load_Script,

		[ShortDescription("PA")]
		[Description("Process Automation")]
		Process_Automation_Script,

		[ShortDescription("LSO")]
		[Description("Life cycle Service Orchestration")]
		Life_Service_Ochestration,

		[ShortDescription("GQIDS")]
		[Description("GQI Data Source")]
		GQI_Data_Source,

		[ShortDescription("GQIO")]
		[Description("GQI Operator")]
		GQI_Operator,

		[ShortDescription("T")]
		[Description("Tests")]
		Tests,

		[ShortDescription("UDAPI")]
		[Description("User-Defined API")]
		User_Defined_API,

		[ShortDescription("DOC")]
		[Description("Documentation")]
		Doc,

		[ShortDescription("DISMACRO")]
		[Description("DIS Macro")]
		DIS_Macro,

		[ShortDescription("CHATOPS")]
		[Description("ChatOps")]
		ChatOps,

		[ShortDescription("S")]
		[Description("Nuget")]
		Nuget,

		[ShortDescription("SC")]
		[Description("Scripted Connector")]
		Scripted_Connector,
	}

	public enum Topics
	{
		[Description("dataminer")]
		DataMiner,

		[Description("dataminer-connector")]
		DataMiner_Connector,

		[Description("dataminer-visio")]
		DataMiner_Visio,

		[Description("dataminer-solution")]
		DataMiner_Solution,

		[Description("dataminer-function")]
		DataMiner_Function,

		[Description("dataminer-automation-script")]
		DataMiner_Automation_Script,

		[Description("dataminer-dashboard")]
		DataMiner_Dashboard,

		[Description("dataminer-profile-load-script")]
		DataMiner_Profile_Load_Script,

		[Description("dataminer-process-automation-script")]
		DataMiner_Process_Automation_Script,

		[Description("dataminer-life-service-orchestration")]
		DataMiner_Life_Service_Ochestration,

		[Description("dataminer-gqi-data-source")]
		DataMiner_GQI_Data_Source,

		[Description("dataminer-gqi-operator")]
		DataMiner_GQI_Operator,

		[Description("dataminer-regression-test")]
		DataMiner_Regression_Test,

		[Description("dataminer-UI-test")]
		DataMiner_UI_Test,

		[Description("dataminer-bot")]
		DataMiner_Bot,

		[Description("dataminer-user-defined-api")]
		DataMiner_User_Defined_API,

		[Description("dataminer-doc")]
		DataMiner_Doc,

		[Description("dataminer-dis-macro")]
		DataMiner_DIS_Macro,

		[Description("dataminer-chatops")]
		DataMiner_ChatOps,

		[Description("dataminer-nuget")]
		DataMiner_Nuget,
	}
}
