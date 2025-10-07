using CE_TaskTest.BackEnd.Services.Dtos.Request;
using FluentValidation;

namespace CE_TaskTest.BackEnd.Services.Validations
{
    public class TareaValidation : AbstractValidator<TareaRequestDto>
    {
        public TareaValidation()
        {
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacío");
            RuleFor(x => x.FechaTarea).NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacío");
            RuleFor(x => x.Visibilidad).NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacío");
            RuleFor(x => x.Estado).NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacío");
            RuleFor(x => x.EstimacionId).NotEmpty().WithMessage("Debes estimar el tiempo necesario");
        }
    }
}
