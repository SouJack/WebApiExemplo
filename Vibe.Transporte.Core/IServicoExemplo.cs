using System;
using System.Threading.Tasks;

namespace Vibe.Transporte.Core
{
    public interface IServicoExemplo
    {
        Task Executa(string id);
    }
}