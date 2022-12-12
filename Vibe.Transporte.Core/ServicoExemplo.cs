using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Threading.Tasks;

namespace Vibe.Transporte.Core
{
    public class ServicoExemplo : IServicoExemplo
    {
        protected ILogger<ServicoExemplo> Logger { get; }

        public ServicoExemplo(ILogger<ServicoExemplo> logger)
        {
            Logger = logger;
        }


        public async Task Executa(string id)
        {
            Policy.Handle<Exception>()
                  .Retry(3, (ex, t) => Logger.LogWarning("[{Id}]Tentativa {Tentativa}. Erro: {Erro}", id, t, ex.Message))
                  .Execute(() =>
                  {
                      var rdn = new Random();
                      var tmp = rdn.Next(1, 999);
                      var resto = tmp % 2;
                      if (resto != 0)
                          throw new Exception("Falha aleatória executando código de exemplo.");
                      Logger.LogInformation("Executou com sucesso {Id}", id);
                  });
            await Task.CompletedTask;
        }
    }
}