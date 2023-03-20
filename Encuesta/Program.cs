using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Rest;


namespace Encuesta
{
    class ApiKeyServiceClientCreds: ServiceClientCredentials
    {
        private readonly string subscriptionKey;
        public ApiKeyServiceClientCreds(string subscriptionKey)
        {
            this.subscriptionKey = subscriptionKey;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            request.Headers.Add("Ocp-Apim-Suscription-Key", this.subscriptionKey);

            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
 
    class Program
    {
        private const string CogServicesSecret = "914fbfa5980947f6b9fe2c3507ad9163";
        //Cambiar WESTUS  a la ubicacioon asignada en la configuracion
        private const string Endpoint = "https://eastus.api.cognitive.microsoft.com";
        static void Main()
        {
            Console.WriteLine("Ejemplo de respuesta de Encuesta");
            DetectSentiment().Wait();
        }


        public static async Task DetectSentiment()
        {
            var credentials = new ApiKeyServiceClientCreds(CogServicesSecret);
            var sentimentMeaning = "";

            var client = new TextAnalyticsClient(credentials)
            {
                Endpoint = Endpoint
            };

            var inputData = new MultiLanguageBatchInput(
                new List<MultiLanguageInput>
                {
                     new MultiLanguageInput("1", "El servicio me gusto demasiado", "es"),
                     new MultiLanguageInput("2", "Muy Mal servico", "es"),
                     new MultiLanguageInput("3", "Como hago para iniciar una atencion", "es")
                });

            var results = await client.SentimentBatchAsync(inputData);

            Console.WriteLine("==========Sentiment Recognition========");

            foreach (var document in results.Documents)
            {
                if (document.Score > 0.5)
                {
                    sentimentMeaning = "Positive";
                }
                else
                {
                    sentimentMeaning = "Negative";
                }

                Console.WriteLine($"Document ID: {document.Id},is {sentimentMeaning}, Sentiment score: {document.Score},  ");
            }

            Console.WriteLine("\n");
        }
        public static void ObjectInfo(DetectResult analysis)
        {
            foreach (var obj in analysis.Objects)
            {
                Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} as location {obj.Rectangle.X}, {obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
            }
            Console.WriteLine("\n");
        }
    }
}


