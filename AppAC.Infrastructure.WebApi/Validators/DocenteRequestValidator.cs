using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class DocenteRequestValidator:AbstractValidator<DocenteRequest>
    {
        public DocenteRequestValidator()
        {
            RuleFor(x => x.Identificacion)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Nombres)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Apellidos)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.departamentoId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .NotNull();
            RuleFor(x => x.Sexo)
                .NotEmpty()
                .NotNull();
        }
    }
}