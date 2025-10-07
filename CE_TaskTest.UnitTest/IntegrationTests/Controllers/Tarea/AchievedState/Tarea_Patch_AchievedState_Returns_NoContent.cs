using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.AchievedState
{
    public class Tarea_Patch_AchievedState_Returns_NoContent
    {
        [Fact]
        public async void Put_AchievedState_Returns_NoContent()
        {
            var mapper = MockTareas.LoadMapper();
            var context = MockTareas.GetInMemoryContextWithData();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();

            // Arrange
            var tarea = new BackEnd.Models.Tarea
            {
                Id = 1,
                Descripcion = "Old",
                FechaTarea = DateOnly.FromDateTime(DateTime.Now),
                Completado = false,
            };

            var controller = new TareaController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.AchievedState(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
