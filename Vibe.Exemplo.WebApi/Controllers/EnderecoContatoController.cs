using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Vibe.Exemplo.WebApi.Modelos;
using Vibe.Transporte.Core.Modelos;

namespace Vibe.Exemplo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoContatoController : ControllerBase
    {
        public IConfiguration Configuracao { get; }
        public EnderecoContatoController(IConfiguration configuracao)
        {
            Configuracao = configuracao;
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> Get(string cpf)
        {
            using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                var retorno = await conexao.QueryFirstOrDefaultAsync<EnderecoContato>(@"select * from VwEndereco where cpf = @cpf", new {cpf});
                if (retorno == null)
                    return NotFound();
                return Ok(retorno);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromHeader] string cpf, [FromForm, FromBody] Endereco endereco)
        {
            await using (SqlConnection conexao = new SqlConnection(Configuracao.GetConnectionString("Conexao")))
            {
                //var p = new {cpf, endereco.Cep, endereco.Logradouro, endereco.Bairro};
                var p = new DynamicParameters();
                p.Add("@cpf",cpf);
                p.Add("@cep", endereco.Cep);
                p.Add("@logradouro", endereco.Logradouro);
                p.Add("@bairro", endereco.Bairro);
                p.Add("@r", direction: ParameterDirection.Output, dbType: DbType.Boolean);
                //var achou = await conexao.QuerySingleAsync<bool>("spAjustaEndereco", p, commandType: CommandType.StoredProcedure);
                await conexao.ExecuteAsync("spAjustaEndereco", p, commandType: CommandType.StoredProcedure);
                var achou = p.Get<bool>("r");
                if (!achou)
                    return NotFound(new ErroResposta {Mensagem = $"Não existe contato para o CPF: {cpf}."});
                return Ok();
            }
        }
    }
}