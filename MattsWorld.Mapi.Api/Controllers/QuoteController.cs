using System.Linq;
using System.Threading.Tasks;
using MattsWorld.Mapi.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MattsWorld.Mapi.Api.Controllers
{
    [Route("[controller]")]
    public class QuoteController : Controller
    {
        private readonly MappingManager _mappingManager;

        public QuoteController()
        {
            IRepository repository = new BlobRepository();
            _mappingManager = new MappingManager(repository);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] QuoteRequest request)
        {
            var outRequest = await _mappingManager.Process(request);

            // This code creates the output structure, should be in it's own class in a different project
            JObject jObject = new JObject();

            foreach (var data in outRequest.Data.OrderBy(x=>x.Key))
            {
                JToken token = jObject.SelectToken(data.Key);
                if (token == null)
                {
                    dynamic jpart = jObject;
                    foreach (var part in data.Key.Split('.'))
                    {
                        if (jpart[part] == null)
                            jpart.Add(new JProperty(part, new JObject()));

                        jpart = jpart[part];
                    }

                    jpart.Replace(data.Value);
                }
                else
                {
                    token.Replace(data.Value);
                }
            }

            var t = jObject.ToString();
            return Ok(t);
        }

   }
    
}
