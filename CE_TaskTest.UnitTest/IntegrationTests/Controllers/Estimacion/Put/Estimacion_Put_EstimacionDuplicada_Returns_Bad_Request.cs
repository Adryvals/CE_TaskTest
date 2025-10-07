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
    public class Estimacion_Put_EstimacionDuplicada_Returns_Bad_Request
    {
        [Fact]
        public async void Put_EstimacionDuplicada_Returns_Bad_Request()
        {
            var mapper = MockEstimacion.LoadMapper();
            var context = MockEstimacion.GetInMemoryContextWithData();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();
            var controller = new EstimacionController(context, mapper, validatorMock.Object);

            // Arrange
            var estimacionRequest = new EstimacionRequestDto
            {
                Duracion = 30,
            };

            validatorMock.Setup(v => v.ValidateAsync(estimacionRequest, default)).ReturnsAsync(
                new ValidationResult()
            );

            // Act
            var result = await controller.Put(1, estimacionRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("La estimacion ya existe", badRequestResult.Value);
        }
    }
}
