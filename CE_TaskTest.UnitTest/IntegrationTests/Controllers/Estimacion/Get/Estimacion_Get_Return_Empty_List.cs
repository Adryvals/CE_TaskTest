using AutoMapper;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.BackEnd.Services.Mappers;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.Get
{
    public class Estimacion_Get_Return_Empty_List
    {
        [Fact]
        public async void Get_Return_Empty_List()
        {
            var context = MockEstimacion.GetEmptyInMemoryContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EstimacionMappingProfile>();
            });
            var mapper = config.CreateMapper();

            // Arrange
            var controller = new EstimacionController(context, mapper);

            // Act
            var result = await controller.Get();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No hay estimaciones registradas.", notFoundResult.Value);
        }
    }
}
