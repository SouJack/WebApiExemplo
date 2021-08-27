namespace Vibe.Transporte.Core.Modelos
{
    public class ErroResposta
    {
        public ErroResposta()
        {
        }
        public ErroResposta(string mensagem, string codigoErro)
        {
            Mensagem = mensagem;
            CodigoErro = codigoErro;
        }

        public string Mensagem { get; set; }
        public string CodigoErro { get; set; }
    }
}