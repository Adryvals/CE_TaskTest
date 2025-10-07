using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.TestSector.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.GetById
{
    public class Estimacion_GetById_Returns_Null
    {
        [Fact]
        public async void Get_Returns_Null()
        {
            var context = MockEstimacion.GetInMemoryContextWithData();

            // Mocks
            var mapper = MockTareas.LoadMapper();
            
            // Arrange
            var controller = new EstimacionController(context, mapper);

            var id = 12;

            // Act
            var result = await controller.GetById(id);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"No se encontró la estimación con ID {id}.", notFound.Value);
        }
    }
}
