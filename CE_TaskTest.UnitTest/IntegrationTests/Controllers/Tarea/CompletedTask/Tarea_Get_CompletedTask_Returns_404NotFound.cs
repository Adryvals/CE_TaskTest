using AutoMapper;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Mappers;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.CompletedTask
{
    public class Tarea_Get_CompletedTask_Returns_404NotFound
    {
        [Fact]
        public void Get_CompletedTask_Returns_NotFound()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TareaMappingProfile>();
            });

            var context = MockTareas.GetEmptyInMemoryContext();

            var mapper = config.CreateMapper();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();
            var service = new TareaService(context);

            // Arrange
            var controller = new TareaController(context, mapper, validatorMock.Object, service);

            // Act
            var result = controller.CompletedTasks();

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("no existen tareas completadas", notFound.Value);
        }
    }
}
