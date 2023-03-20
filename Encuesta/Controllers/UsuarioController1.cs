using Encuesta.Models;
using Google.Cloud.Language.V1;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Encuesta.Controllers
{
    public class UsuarioController1 : Controller
    {
        private readonly HttpClient httpClient;
        private readonly LanguageServiceClient languageService;

        public UsuarioController1(HttpClient httpClient, LanguageServiceClient languageService)
        {
            this.httpClient = httpClient;
            this.languageService = languageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Comment")] Usuario usuario)
        {
            // Create CommentForAnalysis object and serialize to JSON
            var commentForAnalysis = new CommentForAnalysis { Comment = usuario.Comment };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(commentForAnalysis);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Call Natural Language API to analyze sentiment
            var response = await httpClient.PostAsync("https://language.googleapis.com/v1/documents:analyzeSentiment?key=[YOUR_API_KEY]", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var sentimentResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SentimentResponse>(jsonResult);

            // Update usuario object with sentiment information
            usuario.NpsScore = (int)(sentimentResponse.DocumentSentiment.Score * 10) + 10;

            // Save usuario to database or perform any other necessary operations
            // ...

            return RedirectToAction(nameof(Index));
        }
    }
}
