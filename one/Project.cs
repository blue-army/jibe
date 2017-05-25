using System;
using Newtonsoft.Json;

namespace one
{
    public class Project
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

		[JsonProperty(PropertyName = "channel")]
		public string Channel { get; set; }
    }
}
