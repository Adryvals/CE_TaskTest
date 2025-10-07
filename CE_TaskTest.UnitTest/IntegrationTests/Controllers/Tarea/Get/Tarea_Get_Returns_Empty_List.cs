using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.BackEnd.Services.Mappers;
using CE_TaskTest.TestSector.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Get
{
    public class Tarea_Get_Returns_Empty_List
    {
        [Fact]
        public async void Get_Returns_404NotFound()
        {
            var context = MockTareas.GetEmptyInMemoryContext();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TareaMappingProfile>();
            });

            var mapper = config.CreateMapper();

            // Arrange
            var controller = new TareaController(context, mapper);

            // Act
            var result = await controller.Get();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No hay tareas registradas.", notFoundResult.Value);
        }
    }
}
