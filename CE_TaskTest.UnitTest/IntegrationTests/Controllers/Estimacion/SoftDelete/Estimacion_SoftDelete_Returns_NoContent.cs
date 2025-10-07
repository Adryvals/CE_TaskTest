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

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.SoftDelete
{
    public class Estimacion_SoftDelete_Returns_NoContent
    {
        [Fact]
        public async void SoftDelete_Returns_NoContent()
        {
            var context = MockEstimacion.GetInMemoryContextWithData();
            var mapper = MockEstimacion.LoadMapper();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();

            // Arrange
            var controller = new EstimacionController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
