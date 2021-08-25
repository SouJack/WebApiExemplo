using System.Collections.Generic;

namespace Vibe.Transporte.Core.Modelos
{
    public class ErroComDetalhesResposta : ErroResposta
    {
        public List<ErroResposta> DetalhesAdicionais { get; set; }
    }
}