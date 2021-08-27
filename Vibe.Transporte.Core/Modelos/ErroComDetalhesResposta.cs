using System.Collections.Generic;

namespace Vibe.Transporte.Core.Modelos
{
    public class ErroComDetalhesResposta : ErroResposta
    {
        public ErroComDetalhesResposta()
        {
            DetalhesAdicionais = new List<ErroResposta>();
        }
        public ErroComDetalhesResposta(string mensagem, string codigoErro) : base(mensagem, codigoErro)
        {
            DetalhesAdicionais = new List<ErroResposta>();
        }

        public List<ErroResposta> DetalhesAdicionais { get; set; }
    }
}