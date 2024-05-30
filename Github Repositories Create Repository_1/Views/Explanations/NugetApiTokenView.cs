// Ignore Spelling: Github Nuget Api

namespace Skyline.DataMiner.Github.Repositories.Views.Explanations
{
	using System.Text;

	using Skyline.DataMiner.Automation;

	public class NugetApiTokenView : ExplanationInputView
	{
		public NugetApiTokenView(IEngine engine) : base(engine, "Nuget API Token")
		{
			var explanation = new StringBuilder();
			explanation.AppendLine("The API key needed to publish to nuget.org.");
			explanation.AppendLine("For this token you'll need to request it by posting your request in the '04. ECS - NuGet Packages' channel in the 'Expert Hub - Scripts & Connectors' teams on teams.");
			Explanation.Text = explanation.ToString();
		}
	}
}
