using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Web;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;

namespace one
{
    public class MyClass
    {
		public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
		{
            var client = new DocumentClient(new Uri("https://zync.documents.azure.com:443/"), "iR5wKpk619PBuIUqu2b5Dec79sXGsHK1P9jdzvmO1m1YBYeS8spFcP9FscoySw7SvOzka1thg14WNMtgCuRitQ==");

            var doc = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri("jibe", "projects", "977da595-fd0c-f2a5-63c6-edbbcb805e5c"));
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
