using System.Collections.Generic;
using Vibe.Transporte.Core.Modelos;

namespace Vibe.Exemplo.WebApi.Modelos
{
    public class ResultadoValidacao
    {
        public ResultadoValidacao()
        {
            Erros = new List<ErroResposta>();
        }

        public ResultadoValidacao(bool validado) : this()
        {
            Validado = validado;
        }

        public bool Validado { get; set; }
        public List<ErroResposta> Erros { get; set; }

        public void AdicionarErro(string codigo, string mensagem)
        {
            Erros.Add(new ErroResposta(mensagem, codigo));
        }
    }
}