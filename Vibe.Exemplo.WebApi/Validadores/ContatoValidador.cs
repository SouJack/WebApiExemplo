using System;
using System.Linq;
using FluentValidation;
using Vibe.Exemplo.WebApi.Modelos;

namespace Vibe.Exemplo.WebApi.Validadores
{
    public class ContatoValidador : AbstractValidator<Contato>, IValidador<Contato>
    {
        public ContatoValidador()
        {
            RuleFor(c => c.Cpf).NotEmpty()
                               .WithMessage("Preencha o C.P.F.");
            RuleFor(c => c.Nome).NotEmpty()
                                .WithMessage("Preencha o Nome");
            RuleFor(c => c.Nascimento).LessThan(new DateTime(2000, 1, 1))
                                      .WithErrorCode("401")
                                      .WithMessage("{PropertyName} preenchido com {PropertyValue:dd/MM/yyyy} deve ser menor que 01/01/2000");

        }

        public ResultadoValidacao Validar(Contato obj)
        {
            var resultadoPremilinar = Validate(obj);
            var resultado = new ResultadoValidacao(resultadoPremilinar.IsValid);
            resultadoPremilinar.Errors.ForEach(e => resultado.AdicionarErro(e.ErrorCode, e.ErrorMessage));
            return resultado;
        }
    }
}