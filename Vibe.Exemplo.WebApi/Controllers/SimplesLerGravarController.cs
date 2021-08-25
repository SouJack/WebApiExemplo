using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimplesLerGravarController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "valor1", "valor2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
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