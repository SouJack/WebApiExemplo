using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vibe.Exemplo.WebApi.Modelos;
using Vibe.Exemplo.WebApi.Servicos;
using Vibe.Transporte.Core.Modelos;

namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultarCepController : ControllerBase
    {
        public IConsultaCepServico ConsultaCepServico { get; }
        public ConsultarCepController(IConsultaCepServico consultaCepServico)
        {
            ConsultaCepServico = consultaCepServico;
        }

        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(CepResultado), 200)]
        [ProducesResponseType(typeof(ErroResposta), 400)]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> Consultar(string cep)
        {
            var r = await ConsultaCepServico.Consultar(cep);
            return Ok(r);
        }
    }
}