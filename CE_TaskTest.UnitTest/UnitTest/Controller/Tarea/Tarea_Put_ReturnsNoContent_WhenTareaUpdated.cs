using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using FluentValidation;
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
    public class Tarea_Post_ReturnsCreated_WhenTareaValid
    {
        [Fact]
        public async Task Put_ReturnsNoContent_WhenTareaUpdated_AndAnyCheckPasses()
        {
            // Arrange
            var tareas = new List<BackEnd.Models.Tarea>
            {
                new BackEnd.Models.Tarea { Id = 1, Descripcion = "Tarea 1", FechaTarea = new DateOnly(2025, 10, 1), Completado = true },
                new BackEnd.Models.Tarea { Id = 2, Descripcion = "Tarea 2", FechaTarea = new DateOnly(2025, 10, 2), Completado = false }
            };

            var dbSetMock = new Mock<DbSet<BackEnd.Models.Tarea>>();

            // Mock de FindAsync
            dbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                     .ReturnsAsync((object[] ids) => tareas.FirstOrDefault(t => t.Id == (int)ids[0]));

            // Mock de Any() para validación de duplicado
            dbSetMock.As<IQueryable<BackEnd.Models.Tarea>>().Setup(m => m.Provider).Returns(tareas.AsQueryable().Provider);
            dbSetMock.As<IQueryable<BackEnd.Models.Tarea>>().Setup(m => m.Expression).Returns(tareas.AsQueryable().Expression);
            dbSetMock.As<IQueryable<BackEnd.Models.Tarea>>().Setup(m => m.ElementType).Returns(tareas.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<BackEnd.Models.Tarea>>().Setup(m => m.GetEnumerator()).Returns(tareas.AsQueryable().GetEnumerator());

            // Mock del contexto
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Tareas).Returns(dbSetMock.Object);
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var mockMapper = new Mock<IMapper>();
            var updateDto = new TareaRequestDto
            {
                Descripcion = "Tarea 1 Modificada",
                FechaTarea = new DateOnly(2025, 10, 1)
            };
            mockMapper.Setup(m => m.Map(updateDto, It.IsAny<BackEnd.Models.Tarea>()))
                      .Callback<TareaRequestDto, BackEnd.Models.Tarea>((src, dest) =>
                      {
                          dest.Descripcion = src.Descripcion;
                          dest.FechaTarea = src.FechaTarea;
                      });

            var controller = new TareaController(mockContext.Object, mockMapper.Object);

            // Act
            var result = await controller.Put(1, updateDto);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal("Tarea 1 Modificada", tareas.First().Descripcion);
        }
    }
}
