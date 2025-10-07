using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.Put
{
    public class Estimacion_Put_Returns_BadRequest
    {
        [Fact]
        public async void Put_Returns_BadRequest()
        {
            var mapper = MockEstimacion.LoadMapper();
            var context = MockEstimacion.GetInMemoryContextWithData();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();
            var controller = new EstimacionController(context, mapper, validatorMock.Object);

            // Arrange
            var estimacionRequest = new EstimacionRequestDto()
            {
                Duracion = 60
            };

            // Act
            var result = await controller.Put(15, estimacionRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
