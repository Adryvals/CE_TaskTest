using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.BackEnd.Services.Mappers;
using CE_TaskTest.TestSector.MockData;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.Get
{
    public class Tarea_Get_Returns_ListTareas
    {
        [Fact]
        public async void Get_Returns_ListTareas()
        {
            var context = MockTareas.GetInMemoryContextWithData();

            // Mocks
            var mapper = MockTareas.LoadMapper();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();

            // Arrange
            var controller = new TareaController(context, mapper);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedList = Assert.IsType<List<TareaResponseDto>>(okResult.Value);
            Assert.Equal(10, returnedList.Count);
        }
    }
}
