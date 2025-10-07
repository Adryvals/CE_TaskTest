using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Validations
{
    public class EstimacionValidationTest
    {
        [Fact]
        public void EstimacionValidation_Fails_WhenDescripcionIsEmpty()
        {
            var validator = new EstimacionValidation();
            var dto = new EstimacionRequestDto
            {
                Duracion = -15,
            };

            var result = validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Duracion");
        }
    }
}
