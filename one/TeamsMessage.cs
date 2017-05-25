using System;
using Newtonsoft.Json;

namespace one
{
	public class TeamsMessage
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
