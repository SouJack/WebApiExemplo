using Vibe.Exemplo.WebApi.Modelos;

namespace Vibe.Exemplo.WebApi.Validadores
{
    public interface IValidador<T>
    {
        public ResultadoValidacao Validar(T obj);
    }
}