using AppAc.Application;
using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class ActividadRequestValidator:AbstractValidator<ActividadRequest>
    {
        public ActividadRequestValidator()
        {
            RuleFor(x => x.HorasAsignadas)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.IdentificacionAsignador)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.IdentificacionResponsable)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.TipoActividadId)
                .NotNull()
                .NotEmpty();
        }
    }
}