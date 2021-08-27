using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Caching;
using Vibe.Exemplo.WebApi.Modelos;
using Vibe.Exemplo.WebApi.Servicos;
using Vibe.Transporte.Core.Modelos;

namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultarCepController : ControllerBase
    {
        protected IConsultaCepServico ConsultaCepServico { get; }
        protected CachePolicy CachePolicy { get; }
        public ConsultarCepController(IConsultaCepServico consultaCepServico, CachePolicy cachePolicy)
        {
            ConsultaCepServico = consultaCepServico;
            CachePolicy = cachePolicy;
        }

        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(CepResultado), 200)]
        [ProducesResponseType(typeof(ErroResposta), 400)]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> Consultar(string cep)
        {
            var r = await CachePolicy.Execute(async c => await ConsultaCepServico.Consultar(cep), new Context(cep));
            return Ok(r);
        }
    }
}