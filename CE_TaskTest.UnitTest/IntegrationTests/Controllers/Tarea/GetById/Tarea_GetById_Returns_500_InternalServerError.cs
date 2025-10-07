using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.TestSector.MockData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.GetById
{
    public class Tarea_GetById_Returns_500_InternalServerError
    {
        [Fact]
        public async Task GetById_WhenThrowsException_ReturnsInternalServerError()
        {
            var context = MockTareas.GetInMemoryContextWithData();
            var mapper = MockTareas.LoadMapper();

            // Arrange
            // Forzamos una excepción al obtener el contexto o el mapeo de datos
            var controller = new TareaController(null, mapper);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Equal("Error del servidor", statusResult.Value);
        }
    }
}
