using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vibe.Exemplo.WebApi.Modelos;
using Vibe.Transporte.Core;

namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegundoPlanoController : ControllerBase
    {
        protected IExemploOrquestrador Orquestrador { get; }
        protected ILogger<SegundoPlanoController> Logger { get; }

        public SegundoPlanoController(IExemploOrquestrador orquestrador, ILogger<SegundoPlanoController> logger = null)
        {
            Orquestrador = orquestrador;
            Logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(202)]
        public IActionResult Post([FromBody] string idProcesso)
        {
            Orquestrador.SolicitarExecucao(idProcesso);
            return Accepted();
        }
    }
}