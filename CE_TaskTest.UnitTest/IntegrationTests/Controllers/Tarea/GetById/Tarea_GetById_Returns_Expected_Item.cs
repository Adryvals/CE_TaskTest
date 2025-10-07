using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.BackEnd.Services.Mappers;
using CE_TaskTest.TestSector.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.GetById
{
    public class Tarea_GetById_Returns_Expected_Item
    {
        [Fact]
        public async void GetById_Returns_Expected_Item()
        {
            var context = MockTareas.GetInMemoryContextWithData();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TareaMappingProfile>();
            });

            var mapper = config.CreateMapper();

            // Arrange
            var controller = new TareaController(context, mapper);

            var id = 1;

            // Act
            var result = await controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDto = Assert.IsType<TareaResponseDto>(okResult.Value);
            Assert.Equal(1, returnedDto.Id);
            Assert.Equal("Diseñar la arquitectura del sistema", returnedDto.Descripcion);
        }
    }
}
