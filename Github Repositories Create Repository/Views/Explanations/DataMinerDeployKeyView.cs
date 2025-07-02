// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views.Explanations
{
	using System.Text;

	using Skyline.DataMiner.Automation;

	public class DataMinerDeployKeyView : ExplanationInputView
	{
		public DataMinerDeployKeyView(IEngine engine) : base(engine, "DataMiner Deploy Key")
		{
			var explanation = new StringBuilder();
			explanation.AppendLine("A DataMiner Deploy key can be generated or found by going to admin.dataminer.services");
			explanation.AppendLine("Go to https://admin.dataminer.services/ if you don't have a token already.");
			explanation.AppendLine("1. Select from the correct organization from the top right dropdown.");
			explanation.AppendLine("2. Click the cloud connected agents (on the left) you want to deploy to.");
			explanation.AppendLine("3. Click on Keys");
			explanation.AppendLine("4. Click on the new Key button if no key is there.");
			explanation.AppendLine("5. Copy the key by clicking the copy button (doesn't matter if it's the primary or the secondary one. Anyone will do).");
			explanation.AppendLine("6. Paste the token in the below textbox.");
			Explanation.Text = explanation.ToString();
		}
	}
}
