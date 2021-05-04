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
                .NotEmpty();
            RuleFor(x => x.IdentificacionAsignador)
                .NotEmpty();
            RuleFor(x => x.IdentificacionResponsable)
                .NotEmpty();
            RuleFor(x => x.TipoActividadId)
                .NotEmpty();
        }
    }
}