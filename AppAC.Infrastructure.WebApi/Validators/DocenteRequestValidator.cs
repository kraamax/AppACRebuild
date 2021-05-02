using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class DocenteRequestValidator:AbstractValidator<DocenteRequest>
    {
        public DocenteRequestValidator()
        {
            RuleFor(x => x.Identificacion)
                .NotEmpty();
            RuleFor(x => x.Nombres)
                .NotEmpty();
            RuleFor(x => x.Apellidos)
                .NotEmpty();
            RuleFor(x => x.departamentoId)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Sexo)
                .NotEmpty();
        }
    }
}