using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Get
{
    public class Tarea_Get_WhenThrowException_Returns_500_InternalServerError
    {
        [Fact]
        public async Task Get_WhenThrowsException_ReturnsInternalServerError()
        {
            var context = MockTareas.GetInMemoryContextWithData();

            // Mocks
            var mapper = MockTareas.LoadMapper();

            // Arrange
            // Forzamos una excepción al obtener el contexto o el mapeo de datos
            var controller = new TareaController(null, mapper);

            // Act
            var result = await controller.Get();

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Equal("Error del servidor", statusResult.Value);
        }
    }
}
