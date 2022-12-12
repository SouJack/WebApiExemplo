using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimplesLerGravarController : ControllerBase
    {
        protected IMemoryCache MemoryCache { get; }
        protected ILogger<SimplesLerGravarController> Logger { get; }

        public SimplesLerGravarController(IMemoryCache memoryCache, ILogger<SimplesLerGravarController> logger)
        {
            MemoryCache = memoryCache;
            Logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "valor1", "valor2" };
        }

        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            //var l = new List<string>();
            //l.Add(id);  
            //MemoryCache.Set(nameof(IServicoExemplo), l);
            return $"valor{id}";
        }

        [HttpGet("{categoria}/{id}")]
        public string Get(int id, string categoria)
        {
            return $"{categoria}.valor{id}";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}