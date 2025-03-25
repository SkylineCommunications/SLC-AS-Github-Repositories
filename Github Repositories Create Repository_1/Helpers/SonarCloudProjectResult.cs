// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System.Collections.Generic;

	using Newtonsoft.Json;

	// Root myDeserializedClass = SecureNewtonsoftDeserialization.DeserializeObject<Root>(myJsonResponse);
	public class Project
	{
		[JsonProperty("projectKey")]
		public string ProjectKey { get; set; }
	}

	public class SonarCloudProjectResult
	{
		[JsonProperty("projects")]
		public List<Project> Projects { get; set; }
	}
}
