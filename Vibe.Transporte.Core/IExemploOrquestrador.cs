using System;
using System.Threading.Tasks;

namespace Vibe.Transporte.Core
{
    public interface IExemploOrquestrador
    {
        event Func<string, Task> PedidoExecucao;
        void SolicitarExecucao(string id);
    }
}