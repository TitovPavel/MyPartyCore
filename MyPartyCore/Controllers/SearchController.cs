using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DB.Models;
using Nest;
using Newtonsoft.Json;

namespace MyPartyCore.Controllers
{
    public class SearchController : Controller
    {
        private readonly IElasticClient _elasticClient;

        public SearchController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public IActionResult Index(string term)
        {
            var response = _elasticClient.Search<Party>(s => s.AllIndices()
    .AllTypes()
    .From(0)
    .Size(10)
    .Query(q => q
         .MatchPhrasePrefix(m => m
            .Field(f => f.Title)
            .Query(term).MaxExpansions(10)
         )
    )); ;

            var parties = response.Documents.Select(a => new { value = a.Title, label = a.Title }).ToArray();

            return Content(JsonConvert.SerializeObject(parties));
        }
    }
}