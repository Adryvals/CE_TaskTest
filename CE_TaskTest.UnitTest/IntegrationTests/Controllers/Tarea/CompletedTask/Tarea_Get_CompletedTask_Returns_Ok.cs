using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.CompletedTask
{
    public class Tarea_Get_CompletedTask_Returns_Ok
    {
        [Fact]
        public void Get_Completed_Task_Returns_Ok()
        {
            var context = MockTareas.GetInMemoryContextWithData();
            var service = new TareaService(context);
            var mapper = MockTareas.LoadMapper();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();

            // Arrange
            var controller = new TareaController(context, mapper, validatorMock.Object, service);

            // Act
            var result = controller.CompletedTasks();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<List<TareaResponseDto>>(ok.Value);
            Assert.Equal(5, response.Count);
        }
    }
}
