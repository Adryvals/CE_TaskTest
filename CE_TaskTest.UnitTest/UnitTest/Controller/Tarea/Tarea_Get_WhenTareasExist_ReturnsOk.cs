using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace CE_TaskTest.TestSector.UnitTest.Controller.Tarea
{
    public class Estimacion_Get_WhenEstimacionExist_ReturnsOk
    {
        [Fact]
        public async Task Get_WhenTareasExist_ReturnsOk()
        {
            // Arrange
            var tareasMock = new List<BackEnd.Models.Tarea>
            { 
                new BackEnd.Models.Tarea { Id = 1, Descripcion = "Diseñar la arquitectura del sistema", FechaTarea = new DateOnly(2025, 10, 1), Visibilidad = (int)EVisibility.Publico, Estado = 1, EstimacionId = 8, Completado = true },
                new BackEnd.Models.Tarea { Id = 2, Descripcion = "Implementar autenticación JWT", FechaTarea = new DateOnly(2025, 10, 2), Visibilidad = (int)EVisibility.Privado, Estado = 0, EstimacionId = 5, Completado = true },
                new BackEnd.Models.Tarea { Id = 3, Descripcion = "Configurar CI/CD en GitHub Actions", FechaTarea = DateOnly.FromDateTime(DateTime.Now), Visibilidad = (int)EVisibility.Publico, Estado = 2, EstimacionId = 3, Completado = true },
                new BackEnd.Models.Tarea { Id = 4, Descripcion = "Crear pruebas unitarias para UserService", FechaTarea = new DateOnly(2025, 10, 4), Visibilidad = (int)EVisibility.Privado, Estado = 1, EstimacionId = 4, Completado = false },
                new BackEnd.Models.Tarea { Id = 5, Descripcion = "Optimizar consultas con EF Core", FechaTarea = new DateOnly(2025, 10, 5), Visibilidad = (int)EVisibility.Publico, Estado = 0, EstimacionId = 6, Completado = true },
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<List<TareaResponseDto>>(tareasMock))
            .Returns(new List<TareaResponseDto>() {
                new TareaResponseDto { Id = 1, Descripcion = "Diseñar la arquitectura del sistema", FechaTarea = new DateOnly(2025, 10, 1), Visibilidad = (int)EVisibility.Publico, Estado = 1, EstimacionId = 8, Completado = true },
                new TareaResponseDto { Id = 2, Descripcion = "Implementar autenticación JWT", FechaTarea = new DateOnly(2025, 10, 2), Visibilidad = (int)EVisibility.Privado, Estado = 0, EstimacionId = 5, Completado = true },
                new TareaResponseDto { Id = 3, Descripcion = "Configurar CI/CD en GitHub Actions", FechaTarea = DateOnly.FromDateTime(DateTime.Now), Visibilidad = (int)EVisibility.Publico, Estado = 2, EstimacionId = 3, Completado = true },
                new TareaResponseDto { Id = 4, Descripcion = "Crear pruebas unitarias para UserService", FechaTarea = new DateOnly(2025, 10, 4), Visibilidad = (int)EVisibility.Privado, Estado = 1, EstimacionId = 4, Completado = false },
                new TareaResponseDto { Id = 5, Descripcion = "Optimizar consultas con EF Core", FechaTarea = new DateOnly(2025, 10, 5), Visibilidad = (int)EVisibility.Publico, Estado = 0, EstimacionId = 6, Completado = true }
            });

            var contextMock = new Mock<ApplicationDbContext>();
            contextMock.Setup(c => c.Tareas).ReturnsDbSet(tareasMock);

            var controller = new TareaController(contextMock.Object, mapperMock.Object, null, null);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<TareaResponseDto>>(okResult.Value);
            Assert.Equal(5, list.Count);
        }
    }
}
