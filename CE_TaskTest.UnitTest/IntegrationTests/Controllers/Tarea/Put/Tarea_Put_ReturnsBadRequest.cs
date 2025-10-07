using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Put
{
    public class Tarea_Put_ReturnsBadRequest
    {
        [Fact]
        public async Task Put_ReturnsBadRequest()
        {
            var mapper = MockTareas.LoadMapper();
            var context = MockTareas.GetInMemoryContextWithData();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();
            var controller = new TareaController(context, mapper, validatorMock.Object);

            // Arrange
            var tareaRequest = new TareaRequestDto()
            {
                Descripcion = "Implementar notificaciones en tiempo real",
                FechaTarea = new DateOnly(2025, 10, 19)
            };

            // Act
            var result = await controller.Put(15, tareaRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
