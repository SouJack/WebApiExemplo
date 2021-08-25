using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Vibe.Transporte.Core.Modelos;

namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Cria, lê, atualiza e apaga Valores")]
    public class AcoesDetalhadasController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Pegar todos os valores", Description = "Retorna uma lista com todos os valores.")]
        public IEnumerable<string> Get()
        {
            return new[] { "valor1", "valor2" };
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Pegar um valor", Description = "Retorna um valor de acordo com Id passado.")]
        public string Get(int id)
        {
            return "valor";
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Criar um valor", Description = "Criar um valor novo.")]
        public void Post([FromBody, SwaggerRequestBody("Valor novo que vai ser criado", Required = true)] string value)
        {
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Altera um valor", Description = "Altera um determinado valor de acordo com Id passado.")]
        public void Put([SwaggerParameter("Id para alteração", Required = true)] int id, [FromBody, SwaggerRequestBody("Novo conteúdo para valor", Required = true)] string value)
        {
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Apaga um valor", Description = "Apaga um valor de acordo com Id passado.")]
        [SwaggerResponse(200, "Valor foi excluido")]
        [SwaggerResponse(404, "Valor não existe")]
        [SwaggerResponse(400, "Id do Valor não é válido", typeof(ErroResposta))]
        public IActionResult Delete(int id)
        {
            switch (id)
            {
                case <= 0:
                    var resposta = new ErroResposta();
                    resposta.Mensagem = "Valor de Id inválid.";
                    resposta.CodigoErro = "400";
                    return BadRequest(resposta);
                case > 2:
                    return NotFound("Valor não existe");
                default:
                    return Ok();
            }
        }
    }
}