// Ignore Spelling: Github

namespace Skyline.DataMiner.Github.Repositories.Helpers
{
	using System;

	using Newtonsoft.Json;

	// Root myDeserializedClass = JsonConvert.DeserializeObject<SonarCloudGenerateTokenResult>(myJsonResponse);
	public class SonarCloudGenerateTokenResult
	{
		[JsonProperty("login")]
		public string Login { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("token")]
		public string Token { get; set; }

		[JsonProperty("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
