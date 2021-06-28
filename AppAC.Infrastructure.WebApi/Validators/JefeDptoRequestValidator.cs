using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class JefeDptoRequestValidator:AbstractValidator<JefeDptoRequest>
    {
        public JefeDptoRequestValidator()
        {
            RuleFor(x => x.Identificacion)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Nombres)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Apellidos)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.departamentoId)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Sexo)
                .NotNull()
                .NotEmpty();
        }
    }
}