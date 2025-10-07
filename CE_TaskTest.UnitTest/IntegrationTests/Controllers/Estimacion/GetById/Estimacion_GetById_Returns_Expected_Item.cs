using AutoMapper;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.BackEnd.Services.Mappers;
using CE_TaskTest.TestSector.MockData;
using Microsoft.AspNetCore.Mvc;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.GetById
{
    public class Estimacion_GetById_Returns_Expected_Item
    {
        [Fact]
        public async void Get_Returns_Expected_Item()
        {
            var context = MockEstimacion.GetInMemoryContextWithData();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EstimacionMappingProfile>();
            });

            var mapper = config.CreateMapper();

            // Arrange
            var controller = new EstimacionController(context, mapper);

            var id = 1;

            // Act
            var result = await controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDto = Assert.IsType<EstimacionResponseDto>(okResult.Value);
            Assert.Equal(1, returnedDto.Id);
            Assert.Equal(5, returnedDto.Duracion);
        }
    }
}
