using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Validations;
using Xunit;

namespace CE_TaskTest.TestSector.IntegrationTests.Validations
{
    public class TareaValidationTests
    {
        [Fact]
        public void TareaValidation_Fails_WhenDescripcionIsEmpty()
        {
            var validator = new TareaValidation();
            var dto = new TareaRequestDto
            {
                Descripcion = "",
                FechaTarea = new DateOnly(2025, 10, 1),
                Visibilidad = 1,
                Estado = 1,
                EstimacionId = 2,
                Completado = false
            };

            var result = validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Descripcion");
        }
    }
}