using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CE_TaskTest.TestSector.MockData
{
    public static class MockTareas
    {
        public static List<Tarea> LoadTareas()
        {
            var tareas = new List<Tarea>() {
                new Tarea { Id = 1, Descripcion = "Diseñar la arquitectura del sistema", FechaTarea = new DateOnly(2025, 10, 1), Visibilidad = (int)EVisibility.Publico, Estado = 1, EstimacionId = 8, Completado = true },
                new Tarea { Id = 2, Descripcion = "Implementar autenticación JWT", FechaTarea = new DateOnly(2025, 10, 2), Visibilidad = (int)EVisibility.Privado, Estado = 0, EstimacionId = 5, Completado = true },
                new Tarea { Id = 3, Descripcion = "Configurar CI/CD en GitHub Actions", FechaTarea = DateOnly.FromDateTime(DateTime.Now), Visibilidad = (int)EVisibility.Publico, Estado = 2, EstimacionId = 3, Completado = true },
                new Tarea { Id = 4, Descripcion = "Crear pruebas unitarias para UserService", FechaTarea = new DateOnly(2025, 10, 4), Visibilidad = (int)EVisibility.Privado, Estado = 1, EstimacionId = 4, Completado = false },
                new Tarea { Id = 5, Descripcion = "Optimizar consultas con EF Core", FechaTarea = new DateOnly(2025, 10, 5), Visibilidad = (int)EVisibility.Publico, Estado = 0, EstimacionId = 6, Completado = true },
                new Tarea { Id = 6, Descripcion = "Integrar pagos con Stripe", FechaTarea = new DateOnly(2025, 10, 6), Visibilidad = (int)EVisibility.Privado, Estado = 2, EstimacionId = 7, Completado = false },
                new Tarea { Id = 7, Descripcion = "Revisión de seguridad OWASP", FechaTarea = new DateOnly(2025, 10, 7), Visibilidad = (int)EVisibility.Publico, Estado = 1, EstimacionId = 5, Completado = false },
                new Tarea { Id = 8, Descripcion = "Crear documentación de API con Swagger", FechaTarea = new DateOnly(2025, 10, 8), Visibilidad = (int)EVisibility.Publico, Estado = 2, EstimacionId = 2, Completado = false },
                new Tarea { Id = 9, Descripcion = "Implementar notificaciones en tiempo real", FechaTarea = new DateOnly(2025, 10, 9), Visibilidad = (int)EVisibility.Privado, Estado = 0, EstimacionId = 6, Completado = false },
                new Tarea { Id = 10, Descripcion = "Refactorizar código legacy", FechaTarea = new DateOnly(2025, 10, 10), Visibilidad = (int)EVisibility.Publico, Estado = 1, EstimacionId = 4, Completado = true }
            };

            return tareas;
        }

        public static List<Tarea> LoadEmptyTareas()
        {
            return [];
        }

        public static ApplicationDbContext GetEmptyInMemoryContext(string? dbName = null)
        {
            dbName ??= $"EmptyTareasDb_{Guid.NewGuid()}";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
            return context;
        }

        public static ApplicationDbContext GetInMemoryContextWithData(string? dbName = null)
        {
            dbName ??= $"TareasDb_{Guid.NewGuid()}";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            if (!context.Tareas.Any())
            {
                context.Tareas.AddRange(LoadTareas());
                context.SaveChanges();
            }

            return context;
        }

        public static IMapper LoadMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TareaMappingProfile>();
            });

            return config.CreateMapper();
        }
    }
}
