using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vibe.Transporte.Core;

namespace Vibe.Exemplo.WebApi.Processos
{
    public class SegundoPlanoExemplo : BackgroundService
    {
        protected ILogger<SegundoPlanoExemplo> Logger { get; }
        protected IServicoExemplo ServicoExemplo { get; }
        protected IMemoryCache MemoryCache { get; }
        public SegundoPlanoExemplo(ILogger<SegundoPlanoExemplo> logger, IServicoExemplo servicoExemplo, IMemoryCache memoryCache)
        {
            Logger = logger;
            ServicoExemplo = servicoExemplo;
            MemoryCache = memoryCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var temElementos = MemoryCache.TryGetValue<ICollection<string>>(nameof(IServicoExemplo), out var lista);
                if (!temElementos || !lista.Any())
                {
                    //Logger.LogInformation("Executando tarefa em segundo plano, sem elementos para processar.");
                    await Task.Delay(2000, stoppingToken);
                    continue;
                }
                MemoryCache.Remove(nameof(IServicoExemplo));
                foreach (var id in lista)
                {
                    try
                    {
                        await ServicoExemplo.Executa(id);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.LogError(ex, "Nao foi possivel executar {Id}", id);
                    }
                }
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Parada registrada para processo em segundo plano.");
            return base.StopAsync(cancellationToken);
        }
    }
}