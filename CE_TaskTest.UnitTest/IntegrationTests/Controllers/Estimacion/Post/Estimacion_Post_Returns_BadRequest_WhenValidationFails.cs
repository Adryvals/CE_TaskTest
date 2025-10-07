using AutoMapper;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.Post
{
    public class Estimacion_Post_Returns_BadRequest_WhenValidationFails
    {
        [Fact]
        public async void Post_Returns_BadRequest_WhenValidationFails()
        {
            var context = MockEstimacion.GetInMemoryContextWithData();

            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();

            // Arrange
            var estimacionRequest = new EstimacionRequestDto
            {
                Duracion = -60,
            };

            validatorMock.Setup(v => v.ValidateAsync(estimacionRequest, default)).ReturnsAsync(
                new ValidationResult([new ValidationFailure("Duracion", "El campo duracion debe ser mayor que cero")])
            );

            var controller = new EstimacionController(context, mapperMock.Object, validatorMock.Object);

            // Act
            var result = await controller.Post(estimacionRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal("El campo duracion debe ser mayor que cero", errors[0].ErrorMessage);
        }
    }
}
