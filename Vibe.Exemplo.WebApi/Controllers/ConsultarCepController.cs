using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vibe.Exemplo.WebApi.Servicos;

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
        public async Task<IActionResult> Consultar(string cep)
        {
            var r = await ConsultaCepServico.Consultar(cep);
            return Ok(r);
        }
    }
}