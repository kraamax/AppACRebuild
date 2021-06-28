using AppAc.Application;
using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class PlanAccionRequestValidator:AbstractValidator<PlanAccionRequest>
    {
        public PlanAccionRequestValidator()
        {
            RuleFor(x=>x.ActividadId)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Items)
                .NotNull()
                .NotEmpty();
        }
    }
}