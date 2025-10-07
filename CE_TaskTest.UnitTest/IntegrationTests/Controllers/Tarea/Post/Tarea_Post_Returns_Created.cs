using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Post
{
    public class Tarea_Post_Returns_Created
    {
        [Fact]
        public async void Post_Returns_Created()
        {
            var context = MockTareas.GetInMemoryContextWithData();
            var mapper = MockTareas.LoadMapper();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();

            // Arrange
            var tareaRequest = new TareaRequestDto
            {
                Descripcion = "New Features",
                Completado = false,
                EstimacionId = 2,
                Estado = 1,
                FechaTarea = new DateOnly(2025, 10, 1),
                Visibilidad = 1,
            };

            validatorMock.Setup(v => v.ValidateAsync(tareaRequest, default)).ReturnsAsync(
                new ValidationResult()
            );

            var controller = new TareaController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.Post(tareaRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
