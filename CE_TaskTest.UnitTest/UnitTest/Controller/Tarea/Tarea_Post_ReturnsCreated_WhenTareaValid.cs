using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.UnitTest.Controller.Tarea
{
    public class Estimacion_Post_ReturnsCreated_WhenTareaValid
    {
        [Fact]
        public async Task Post_ReturnsCreated_WhenTareaValid()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<TareaRequestDto>>();

            var tareas = new List<BackEnd.Models.Tarea>
            {
                new BackEnd.Models.Tarea { Id = 1, Descripcion = "Tarea 1", Completado = true },
                new BackEnd.Models.Tarea { Id = 2, Descripcion = "Tarea 2", Completado = false }
            };

            // Arrange
            var newTareaDto = new TareaRequestDto { Descripcion = "Nueva Tarea" };
            mockValidator.Setup(v => v.ValidateAsync(newTareaDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            mockMapper.Setup(m => m.Map<BackEnd.Models.Tarea>(newTareaDto)).Returns(new BackEnd.Models.Tarea { Id = 3, Descripcion = "Nueva Tarea" });
            mockContext.Setup(c => c.Tareas).ReturnsDbSet(tareas);

            var controller = new TareaController(mockContext.Object, mockMapper.Object, mockValidator.Object, null);

            // Act
            var result = await controller.Post(newTareaDto);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result);
            var tarea = Assert.IsType<BackEnd.Models.Tarea>(created.Value);
            Assert.Equal("Nueva Tarea", tarea.Descripcion);
        }
    }
}
