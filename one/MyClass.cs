using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Web;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Collections;

namespace one
{
    public class MyClass
    {
		public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
		{
            var db_key = System.Environment.GetEnvironmentVariable("db_key");

            var client = new DocumentClient(new Uri("https://zync.documents.azure.com:443/"), db_key);
            var doc = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri("jibe", "projects", "7aff6973-64a0-9cce-d767-a4c01622d7ed"), new RequestOptions());
            log.Info(doc.Resource.ToString());

			log.Info("C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

			// parse query parameter
			string name = req.GetQueryNameValuePairs()
				.FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
				.Value;

			// Get request body
			dynamic data = await req.Content.ReadAsAsync<object>();

			// Set name to query string or body data
			name = name ?? data?.name;

			return name == null
				? req.CreateResponse(HttpStatusCode.BadRequest, "Please provide a name on the query string or in the request body")
				: req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
		}
    }
}
