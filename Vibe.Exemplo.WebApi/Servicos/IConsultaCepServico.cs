using System.Threading.Tasks;
using Refit;
using Vibe.Exemplo.WebApi.Modelos;

namespace Vibe.Exemplo.WebApi.Servicos
{
    public interface IConsultaCepServico
    {
        [Get("/ws/{cep}/json/")]
        Task<CepResultado> Consultar(string cep);
    }
}