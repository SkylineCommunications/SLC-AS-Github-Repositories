// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views.Explanations
{
	using System.Text;

	using Skyline.DataMiner.Automation;

	public class GithubTokenView : ExplanationInputView
	{
		public GithubTokenView(IEngine engine) : base(engine, "Github Token")
		{
			var explanation = new StringBuilder();
			explanation.AppendLine("A Github Token can be generated or found by going to github.com");
			explanation.AppendLine("Go to https://github.com/settings/profile if you don't have a token already.");
			explanation.AppendLine("1. Click on 'Developer settings' on the bottom left of the options.");
			explanation.AppendLine("2. Click on 'Personal access tokens'.");
			explanation.AppendLine("3. Click on 'Tokens (classic)'.");
			explanation.AppendLine("4. Click on 'Generate new token'.");
			explanation.AppendLine("5. Click on 'Generate new (classic)'.");
			explanation.AppendLine("6. Fill in the name of the repository or the nuget package name in the 'Note' field.");
			explanation.AppendLine("7. Choose the No expiration default option.");
			explanation.AppendLine("8. Select the write:packages and delete:packages scopes. The repo scope should be selected by default as well.");
			explanation.AppendLine("9. Click Generate token in the lower left corner.");
			explanation.AppendLine("10. Paste the token in the below textbox.");
			Explanation.Text = explanation.ToString();
		}
	}
}
