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

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.Post
{
    public class Estimacion_Post_Returns_Created
    {
        [Fact]
        public async void Post_Returns_Created()
        {
            var context = MockEstimacion.GetInMemoryContextWithData();
            var mapper = MockEstimacion.LoadMapper();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();

            // Arrange
            var estimacionRequest = new EstimacionRequestDto
            {
                Duracion = 60
            };

            validatorMock.Setup(v => v.ValidateAsync(estimacionRequest, default)).ReturnsAsync(
                    new FluentValidation.Results.ValidationResult()
                );

            var controller = new EstimacionController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.Post(estimacionRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
