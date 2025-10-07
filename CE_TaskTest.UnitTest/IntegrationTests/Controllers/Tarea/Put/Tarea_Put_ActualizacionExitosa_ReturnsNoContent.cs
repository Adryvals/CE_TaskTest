using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Put
{
    public class Tarea_Put_ActualizacionExitosa_ReturnsNoContent
    {
        [Fact]
        public async Task Put_ActualizacionExitosa_ReturnsNoContent()
        {
            var mapper = MockTareas.LoadMapper();
            var context = MockTareas.GetInMemoryContextWithData();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();

            // Arrange
            var tareaExistente = new BackEnd.Models.Tarea
            {
                Id = 1,
                Descripcion = "Old",
                FechaTarea = DateOnly.FromDateTime(DateTime.Now),
            };
            var tareaRequest = new TareaRequestDto
            {
                Descripcion = "New",
                FechaTarea = DateOnly.FromDateTime(DateTime.Now),
                Estado = (int)EEstado.Normal,
                EstimacionId = 2,
                Completado = true,
                Visibilidad = (int)EVisibility.Privado
            };

            var controller = new TareaController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.Put(1, tareaRequest);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal("New", tareaRequest.Descripcion);
            Assert.Equal((int)EEstado.Normal, tareaRequest.Estado);
            Assert.True(tareaRequest.Completado);
        }
    }
}
