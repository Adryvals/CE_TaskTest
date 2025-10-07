using CE_TaskTest.BackEnd.Services.Dtos.Request;
using FluentValidation;

namespace CE_TaskTest.BackEnd.Services.Validations
{
    public class EstimacionValidation : AbstractValidator<EstimacionRequestDto>
    {
        public EstimacionValidation()
        {
            RuleFor(x => x.Duracion).GreaterThan(0).WithMessage("La duracion debe ser mayor a 0");
        }
    }
}
