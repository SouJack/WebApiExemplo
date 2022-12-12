using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Vibe.Transporte.Core
{
    public class ExemploOrquestrador : IExemploOrquestrador
    {
        protected ILogger<ExemploOrquestrador> Logger { get; }
        public ExemploOrquestrador(ILogger<ExemploOrquestrador> logger)
        {
            Logger = logger;
        }

        public event Func<string, Task> PedidoExecucao;
        public async void SolicitarExecucao(string id)
        {
            await Task.Delay(1000);
            PedidoExecucao?.Invoke(id);
        }
    }
}