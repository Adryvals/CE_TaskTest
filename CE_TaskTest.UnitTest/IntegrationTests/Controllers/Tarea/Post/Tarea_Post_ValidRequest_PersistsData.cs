using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Post
{
    public class Tarea_Post_ValidRequest_PersistsData
    {
        [Fact]
        public async Task Post_ValidRequest_PersistsData()
        {
            using var context = MockTareas.GetInMemoryContextWithData();

            // Mocks
            var mapper = MockTareas.LoadMapper();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();

            var tareaRequest = new TareaRequestDto
            {
                Descripcion = "Configurar CI/CD en GitHub Actions",
                Estado = (int)EEstado.Normal,
                EstimacionId = 1,
                Completado = false,
                FechaTarea = new DateOnly(2025, 10, 3),
                Visibilidad = (int)EVisibility.Publico,
            };

            validatorMock.Setup(v => v.ValidateAsync(tareaRequest, default)).ReturnsAsync(
                new ValidationResult()
            );

            // Crear controlador
            var controller = new TareaController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.Post(tareaRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.True(context.Tareas.Any(t =>
                t.Descripcion == tareaRequest.Descripcion &&
                t.FechaTarea == tareaRequest.FechaTarea
            ));
        }
    }
}
