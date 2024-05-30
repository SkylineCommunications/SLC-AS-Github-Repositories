// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Views.Explanations
{
	using System.Text;

	using Skyline.DataMiner.Automation;

	public class SonarCloudProjectIdView : ExplanationInputView
	{
		public SonarCloudProjectIdView(IEngine engine) : base(engine, "Sonar Cloud Project ID")
		{
			var explanation = new StringBuilder();
			explanation.AppendLine("The project id of the repository retrieved from sonarcloud.io.");
			explanation.AppendLine("Go to https://sonarcloud.io/projects/create if the repository is not in there already.");
			explanation.AppendLine("1. Select the correct Organization form the dropdown.");
			explanation.AppendLine("2. Select the correct Repository form the list.");
			explanation.AppendLine("3. Click on the 'Set Up' button");
			explanation.AppendLine("4. Select previous version");
			explanation.AppendLine("5. Click on create project");
			explanation.AppendLine("6. In the URL of your browser you will find the Sonar Cloud Project ID, Copy the id without the 'id='");
			explanation.AppendLine("7. Paste the id in the below textbox.");
			Explanation.Text = explanation.ToString();
		}
	}
}
