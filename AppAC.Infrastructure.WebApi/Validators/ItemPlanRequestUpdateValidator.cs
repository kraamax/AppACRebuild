using AppAc.Application;
using AppAC.Application;
using FluentValidation;

namespace AppAC.Infrastructure.WebApi.Validators
{
    public class ItemPlanRequestUpdateValidator:AbstractValidator<ItemPlanUpdateRequest>
    {
        public ItemPlanRequestUpdateValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty();
            RuleFor(x=>x.AccionPlaneada_Descripcion)
                .NotEmpty();
            RuleFor(x => x.AccionRealizada_Descripcion).NotEmpty();
            RuleFor(x => x.AccionRealizada_evidencia_Ruta).NotEmpty();

        }
    }
}