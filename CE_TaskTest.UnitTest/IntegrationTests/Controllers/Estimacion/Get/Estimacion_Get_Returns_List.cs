using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Estimacion.Get
{
    public class Estimacion_Get_Returns_List
    {
        [Fact]
        public async void Get_Returns_List()
        {
            var context = MockEstimacion.GetInMemoryContextWithData();

            // Mocks
            var mapper = MockEstimacion.LoadMapper();
            var validatorMock = new Mock<IValidator<EstimacionRequestDto>>();

            // Arrange
            var controller = new EstimacionController(context, mapper, validatorMock.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedList = Assert.IsType<List<EstimacionResponseDto>>(okResult.Value);
            Assert.Equal(8, returnedList.Count);
        }
    }
}
