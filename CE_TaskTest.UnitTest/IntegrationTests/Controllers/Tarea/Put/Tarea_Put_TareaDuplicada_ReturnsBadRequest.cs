using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Put
{
    public class Tarea_Put_TareaDuplicada_ReturnsBadRequest
    {
        [Fact]
        public async Task Put_TareaDuplicada_ReturnsBadRequest()
        {
            var mapper = MockTareas.LoadMapper();
            var context = MockTareas.GetInMemoryContextWithData();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();
            var controller = new TareaController(context, mapper, validatorMock.Object);

            // Arrange
            var tareaRequest = new TareaRequestDto
            {
                Descripcion = "Diseñar la arquitectura del sistema",
                FechaTarea = new DateOnly(2025, 10, 1)
            };

            // Act
            var result = await controller.Put(10, tareaRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("La tarea ya existe", badRequestResult.Value);
        }
    }
}
