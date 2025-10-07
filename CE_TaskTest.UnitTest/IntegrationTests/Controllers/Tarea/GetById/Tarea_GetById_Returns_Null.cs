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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Controllers.Tarea.GetById
{
    public class Tarea_GetById_Returns_Null
    {
        [Fact]
        public async void GetById_Returns_404NotFound()
        {
            var context = MockTareas.GetInMemoryContextWithData();

            // Mocks
            var service = new TareaService(context);

            var mapper = MockTareas.LoadMapper();
            var validatorMock = new Mock<IValidator<TareaRequestDto>>();

            // Arrange
            var controller = new TareaController(context, mapper, validatorMock.Object, service);

            var id = 12;

            // Act
            var result = await controller.GetById(id);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"No se encontró la tarea con ID {id}.", notFound.Value);
        }
    }
}
