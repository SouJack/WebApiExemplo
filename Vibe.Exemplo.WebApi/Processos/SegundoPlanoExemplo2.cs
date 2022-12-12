using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Vibe.Transporte.Core;

namespace Vibe.Exemplo.WebApi.Processos
{
    public class SegundoPlanoExemplo2 : BackgroundService
    {
        protected ILogger<SegundoPlanoExemplo2> Logger { get; }
        protected IExemploOrquestrador Orquestrador { get; }

        public SegundoPlanoExemplo2(ILogger<SegundoPlanoExemplo2> logger, IExemploOrquestrador orquestrador)
        {
            Logger = logger;
            Orquestrador = orquestrador;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Orquestrador.PedidoExecucao += ExecutandoPedido;
            return Task.CompletedTask;
        }

        private async Task ExecutandoPedido(string id)
        {
            try
            {
                var pollyExec = Policy.Handle<Exception>()
                                      .RetryAsync(3, (ex, t) => Logger.LogWarning("[{Id}]Tentativa {Tentativa}. Erro: {Erro}", id, t, ex.Message));
                await pollyExec.ExecuteAsync(async () =>
                         {
                             Logger.LogInformation("Rodando processo longo para {Id}...", id);
                             await Task.Delay(6000);
                             var rdn = new Random();
                             var tmp = rdn.Next(1, 999);
                             var resto = tmp % 2;
                             if (resto != 0)
                             {
                                 throw new Exception("Falha aleatória executando código de exemplo.");
                             }
                             Logger.LogInformation("Executado com sucesso {Id}.", id);
                         });
            }
            catch (Exception ex)
            {
                //Processos em segundo plano não podem soltar exceção senão eles são interrompidos, nesse caso, apenas logar.
                Logger.LogError("#### FALHA NO PROCESSO SEGUNDO PLANO ####", ex);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Parada registrada para processo em segundo plano.");
            Orquestrador.PedidoExecucao -= ExecutandoPedido;
            return base.StopAsync(cancellationToken);
        }
    }
}