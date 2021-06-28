using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class TipoActividadRequestValidator:AbstractValidator<TipoActividadRequest>
    {
        public TipoActividadRequestValidator()
        {
            RuleFor(x=>x.Nombre)
                .NotNull()
                .NotEmpty()
                ;
        }
    }
}