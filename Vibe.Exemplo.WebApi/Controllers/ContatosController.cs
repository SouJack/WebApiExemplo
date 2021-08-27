using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Vibe.Exemplo.WebApi.Modelos;
using Vibe.Exemplo.WebApi.Validadores;
using Vibe.Transporte.Core.Modelos;


namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        protected IConfiguration Configuracao { get; }
        protected IValidador<Contato> Validador { get; }
        public ContatosController(IConfiguration configuracao, IValidador<Contato> validador)
        {
            Configuracao = configuracao;
            Validador = validador;
        }

        [HttpGet]
        public IEnumerable<Contato> Get()
        {
            using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                var lista = conexao.GetAll<Contato>();
                return lista;
            }
        }

        [HttpGet("{id}")]
        public Contato Get(int id)
        {
            using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                var contato = conexao.Get<Contato>(id);
                return contato;
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Contato), 200)]
        [ProducesResponseType(typeof(ErroComDetalhesResposta), 400)]
        public IActionResult Post([FromBody] Contato contato)
        {
            //var validador = new ContatoValidador();
            var validacao = Validador.Validar(contato);
            if (!validacao.Validado)
            {
                var resposta = new ErroComDetalhesResposta("400", "Contato inválido, veja os detalhes.")
                {
                        DetalhesAdicionais = validacao.Erros
                };
                return BadRequest(resposta);
            }
            using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                var id = conexao.Insert(contato);
                contato.Id = (int) id;
                return Ok(contato);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Contato), 200)]
        [ProducesResponseType(typeof(ErroComDetalhesResposta), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Put([FromBody] Contato contato)
        {
            var validador = new ContatoValidador();
            var validacao = validador.Validate(contato);
            if (!validacao.IsValid)
            {
                var resposta = new ErroComDetalhesResposta("400", "Contato inválido, veja os detalhes.")
                {
                        DetalhesAdicionais = validacao.Errors.Select(e => new ErroResposta(e.ErrorMessage, e.ErrorCode)).ToList()
                };
                return BadRequest(resposta);
            }
            using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                var achou = conexao.Update(contato);
                if (!achou)
                    return NotFound("Não achei seu registro.");
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                conexao.Delete(new Contato {Id = id});
            }
        }
    }
}