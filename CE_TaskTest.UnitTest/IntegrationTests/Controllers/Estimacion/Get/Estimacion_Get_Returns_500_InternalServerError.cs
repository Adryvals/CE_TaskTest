using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.TestSector.MockData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.Get
{
    public class Estimacion_Get_Returns_500_InternalServerError
    {
        [Fact]
        public async void Get_Returns_500_InternalServerError()
        {
            var context = MockEstimacion.GetInMemoryContextWithData();
            var mapper = MockEstimacion.LoadMapper();

            // Arrange
            var controller = new EstimacionController(null, mapper);

            // Act
            var result = await controller.Get();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Error del servidor", objectResult.Value);
        }
    }
}
