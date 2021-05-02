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
                .NotEmpty();
            RuleFor(x => x.Items).NotEmpty();
        }
    }
}