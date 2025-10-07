using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.Put
{
    public class Estimacion_Put_BadRequest_WhenValidation_Fails
    {
        [Fact]
        public async void Put_BadRequest_WhenValidation_Fails()
        {
            var mapper = MockEstimacion.LoadMapper();
            var context = MockEstimacion.GetInMemoryContextWithData();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();

            // Arrange
            var estimacionRequest = new EstimacionRequestDto()
            {
                Duracion = -60
            };

            validatorMock.Setup(v => v.ValidateAsync(estimacionRequest, default)).ReturnsAsync(
                new ValidationResult([new ValidationFailure("Duracion", "El valor del campo duracion debe ser mayor que 0")])
            );

            var controller = new EstimacionController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.Put(1, estimacionRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal("El valor del campo duracion debe ser mayor que 0", errors.First().ErrorMessage);
        }
    }
}
