using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CE_TaskTest.TestSector.MockData
{
    public  static class MockEstimacion
    {
        public static List<Estimacion> LoadEstimaciones()
        {
            var estimaciones = new List<Estimacion>() {
                new() { Id = 1, Duracion = 5, Activo = true },
                new() { Id = 2, Duracion = 10, Activo = true },
                new() { Id = 3, Duracion = 15, Activo = true },
                new() { Id = 4, Duracion = 20, Activo = true },
                new() { Id = 5, Duracion = 25, Activo = true },
                new() { Id = 6, Duracion = 30, Activo = true },
                new() { Id = 7, Duracion = 40, Activo = true },
                new() { Id = 8, Duracion = 50, Activo = true }
            };

            return estimaciones;
        }

        public static List<Estimacion> LoadEmptyEstimaciones()
        {
            return [];
        }

        public static ApplicationDbContext GetEmptyInMemoryContext(string? dbName = null)
        {
            dbName ??= $"EmptyEstimacionesDb_{Guid.NewGuid()}";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureDeleted(); 

            return context;
        }

        public static ApplicationDbContext GetInMemoryContextWithData(string? dbName = null)
        {
            dbName ??= $"EstimacionesDb_{Guid.NewGuid()}";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            if (!context.Estimaciones.Any())
            {
                context.Estimaciones.AddRange(LoadEstimaciones());
                context.SaveChanges();
            }

            return context;
        }

        public static IMapper LoadMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EstimacionMappingProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
