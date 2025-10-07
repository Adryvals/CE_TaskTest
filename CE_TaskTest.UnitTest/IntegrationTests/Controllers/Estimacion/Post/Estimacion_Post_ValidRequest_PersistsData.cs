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
    public class Estimacion_Post_ValidRequest_PersistsData
    {
        [Fact]
        public async void Post_ValidRequest_PersistsData()
        {
            using var context = MockEstimacion.GetInMemoryContextWithData();

            // Mocks
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();

            var estimacionRequest = new EstimacionRequestDto
            {
                Duracion = 50
            };

            validatorMock.Setup(v => v.ValidateAsync(estimacionRequest, default)).ReturnsAsync(
                new ValidationResult()
            );

            var controller = new EstimacionController(context, mapperMock.Object, validatorMock.Object);

            // Act
            var result = await controller.Post(estimacionRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.True(context.Estimaciones.Any(t =>
                t.Duracion == estimacionRequest.Duracion
            ));
        }
    }
}
