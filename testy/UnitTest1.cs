using System;
using Xunit;
using Microsoft.Azure.Documents.Client;

namespace testy
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var client = new DocumentClient(new Uri("https://zync.documents.azure.com:443/"), "");
            var doc = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri("jibe", "projects", "7aff6973-64a0-9cce-d767-a4c01622d7ed"));

            Console.WriteLine(doc.Resource);

            var collectionLink = UriFactory.CreateDocumentCollectionUri("jibe", "projects");
            var docs = await client.ReadDocumentFeedAsync(collectionLink, new FeedOptions { MaxItemCount = 10 });
            foreach (var d in docs)
            {
                Console.WriteLine(d);
            }
        }
    }
}
