using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Newtonsoft.Json;
using System;

namespace Bing
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionKey = "5783dbc5562e4297a79a3e9f2f0aca2c";

            Console.WriteLine("Digite o nome de uma personalidade: "); ;
            var nome = Console.ReadLine();

            var client = new WebSearchClient(new ApiKeyServiceClientCredentials(subscriptionKey));

            var result = client.Web.SearchAsync(nome).Result;
            var json = JsonConvert.SerializeObject(result);


            Console.WriteLine(json);

            Console.Read();
        }
    }
}
