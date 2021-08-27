using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Vibe.Exemplo.WebApi.Modelos;


namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        public IConfiguration Configuracao { get; }
        public ContatosController(IConfiguration configuracao)
        {
            Configuracao = configuracao;
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
        public IActionResult Post([FromBody] Contato contato)
        {
            using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                var id = conexao.Insert(contato);
                contato.Id = (int) id;
                return Ok(contato);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Contato contato)
        {
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