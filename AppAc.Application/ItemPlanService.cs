using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using AppAC.Application;

namespace AppAc.Application
{
    public class ItemPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlanAccionRepository _planAccionRepository;
        private readonly IPlazoAperturaRepository _plazoAperturaRepository;
        private readonly IItemPlanRepository _itemPlanRepository;
        private VerificarPlazoAperturaService verificarPlazoAperturaService;


        public ItemPlanService(
           IUnitOfWork unitOfWork,
           IPlanAccionRepository planAccionRepository,
           IItemPlanRepository itemPlanRepository,
           IPlazoAperturaRepository plazoAperturaRepository
       )
        {
            _unitOfWork = unitOfWork;
            _itemPlanRepository = itemPlanRepository;
            _planAccionRepository = planAccionRepository;
            _plazoAperturaRepository = plazoAperturaRepository;
            verificarPlazoAperturaService = new VerificarPlazoAperturaService(_plazoAperturaRepository);
        }
        public ItemPlanResponse RegistrarItem(ItemPlanRequest request)
        {

            var planAccion = _planAccionRepository.Find(request.PlanId);
            if (planAccion == null)
                return new ItemPlanResponse("No se encontró el plan de acción","Error",null);
            var verificacionPlazoApertura = verificarPlazoAperturaService.Handle(planAccion.Actividad.Asignador.Identificacion);
            if (verificacionPlazoApertura.Contains("Error"))
                return new ItemPlanResponse(verificacionPlazoApertura,"Error", null);
            
            var errors = ItemPlanUtils.CanConvertToItemPlan(request);
            if (errors.Any())
            {
                var result = String.Join(", ", errors.ToArray());
                return new ItemPlanResponse(result,"Error", null);
            }
            var item = ItemPlanUtils.ConvertToItemPlan(request);
            var response = "";
            try
            {
                _itemPlanRepository.Add(item);
                response = "Item registrado correctamente";
            }
            catch (Exception e)
            {
                return  new ItemPlanResponse("No se pudo registrar","Error",null);
            }
            _unitOfWork.Commit();
            return new ItemPlanResponse(response,"Ok",item);
        }

        public ItemPlanResponse EliminarItem(int id)
        {
            var item = _itemPlanRepository.Find(id);

            if (item == null)
                return new ItemPlanResponse("No se encontró el item","Error",item);
            var plan = _planAccionRepository.Find(item.PlanAccionId);
            var verificacionPlazoApertura = verificarPlazoAperturaService.Handle(plan.Actividad.Asignador.Identificacion);
            if (verificacionPlazoApertura.Contains("Error"))
                return new ItemPlanResponse(verificacionPlazoApertura,"Error", null);
            _itemPlanRepository.Delete(item);
            _unitOfWork.Commit();
            return new ItemPlanResponse("Se elimino el item","Ok",item);
        }

        public ItemPlanResponse ModificarItem(ItemPlanUpdateRequest request)
        {
            var item = _itemPlanRepository.Find(request.Id);
            if (item == null)
                return new ItemPlanResponse("No se encontro el item","Error", null);
            var plan = _planAccionRepository.Find(item.PlanAccionId);
            var verificacionPlazoApertura = verificarPlazoAperturaService.Handle(plan.Actividad.Asignador.Identificacion);
            if (verificacionPlazoApertura.Contains("Error"))
                return new ItemPlanResponse(verificacionPlazoApertura,"Error", null);
            var errors = new List<string>();
            errors.AddRange(ItemPlanUtils.CanConvertToItemPlan(request));
            if (errors.Any())
                return new ItemPlanResponse(StringUtils.ToString(errors),"Error", item);
            var auxItem = ItemPlanUtils.ConvertToItemPlan(request);
            errors.AddRange(item.CanDeliver(auxItem.AccionPlaneada, auxItem.AccionRealizada));
            if (errors.Any())
                return new ItemPlanResponse(StringUtils.ToString(errors),"Error", item);
            item.Deliver(auxItem.AccionPlaneada,auxItem.AccionRealizada,item.PlanAccionId);
            var response = "";
            try
            {
                _itemPlanRepository.Update(item);
                response = "Se actualizó el item correctamente";
            }
            catch (Exception e)
            {
                return new ItemPlanResponse( "No se pudo actualizar","Ok",null);
            }
            _unitOfWork.Commit();
            return new ItemPlanResponse(response, "Ok", item);
        }
    }
    public record ItemPlanRequest(int PlanId, string AccionPlaneada_Descripcion,string AccionRealizada_Descripcion, string AccionRealizada_evidencia_Ruta);
    public record ItemPlanUpdateRequest(int Id, string AccionPlaneada_Descripcion,string AccionRealizada_Descripcion, string AccionRealizada_evidencia_Ruta);
    public record ItemPlanResponse(string Message, string MessageType, ItemPlan ItemPlan);

   
    
}
