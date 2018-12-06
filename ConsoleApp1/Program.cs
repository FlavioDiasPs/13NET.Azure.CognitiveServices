
using Microsoft.Azure.CognitiveServices.Vision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Newtonsoft.Json;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionKey = "002d9cf2be714679abeedb67f0bf137a";
            var imageUrl = @"https://cdn-istoedinheiro-ssl.akamaized.net/wp-content/uploads/sites/17/2018/02/palio.jpg";

            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey));
            client.Endpoint = "https://brazilsouth.api.cognitive.microsoft.com/";

            var result = client.AnalyzeImageAsync(imageUrl).Result;

            Console.WriteLine(JsonConvert.SerializeObject(result));

            Console.ReadLine();
        }
    }
}
