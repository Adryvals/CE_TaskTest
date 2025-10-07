using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.UnitTest.Controller.Tarea
{
    public class Tarea_GetById_ReturnsOk_WhenTareaExists
    {
        [Fact]
        public async Task GetById_ReturnsOk_WhenTareaExists()
        {
            // Arrange
            var tareasMock = new List<BackEnd.Models.Tarea>
            {
                new BackEnd.Models.Tarea { Id = 1, Descripcion = "Tarea 1", FechaTarea = new DateOnly(2025, 10, 1), Visibilidad = (int)EVisibility.Publico, Estado = 1, EstimacionId = 8, Completado = true },
                new BackEnd.Models.Tarea { Id = 2, Descripcion = "Tarea 2", FechaTarea = new DateOnly(2025, 10, 2), Visibilidad = (int)EVisibility.Privado, Estado = 0, EstimacionId = 5, Completado = true }
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<TareaResponseDto>(It.IsAny<BackEnd.Models.Tarea>()))
                      .Returns((BackEnd.Models.Tarea tarea) => new TareaResponseDto
                      {
                          Id = tarea.Id, Descripcion = tarea.Descripcion, FechaTarea = tarea.FechaTarea, Visibilidad = tarea.Visibilidad, Estado = tarea.Estado, EstimacionId = tarea.EstimacionId, Completado = tarea.Completado
                      });


            var dbSetMock = new Mock<DbSet<BackEnd.Models.Tarea>>();

            dbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                     .ReturnsAsync((object[] ids) => tareasMock.FirstOrDefault(t => t.Id == (int)ids[0]));

            var contextMock = new Mock<ApplicationDbContext>();
            contextMock.Setup(c => c.Tareas).Returns(dbSetMock.Object);

            var controller = new TareaController(contextMock.Object, mapperMock.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<TareaResponseDto>(okResult.Value);
            Assert.Equal(1, dto.Id);
        }
    }
}
