using AutoMapper;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Mappers;
using System.Threading;
using Xunit;

namespace CE_TaskTest.TestSector.UnitTests.Mapping
{
    public class TareaMappingTests
    {
        [Fact]
        public void TareaRequestDto_To_Tarea_Mapping_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TareaMappingProfile>();
            });

            var mapper = config.CreateMapper();

            var dto = new TareaRequestDto
            {
                Descripcion = "Test tarea",
                FechaTarea = new DateOnly(2025, 10, 1),
                Visibilidad = 1,
                Estado = 1,
                EstimacionId = 2,
                Completado = false
            };

            var tarea = mapper.Map<Tarea>(dto);

            Assert.Equal(dto.Descripcion, tarea.Descripcion);
            Assert.Equal(dto.FechaTarea, tarea.FechaTarea);
            Assert.Equal(dto.Visibilidad, tarea.Visibilidad);
            Assert.Equal(dto.Estado, tarea.Estado);
            Assert.Equal(dto.EstimacionId, tarea.EstimacionId);
            Assert.Equal(dto.Completado, tarea.Completado);
        }

        [Fact]
        public void ListTareaRequest_To_ListTarea_Mapping_IsValid()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<TareaMappingProfile>(); });

            var mapper = config.CreateMapper();

            var listDtos = new List<TareaRequestDto>();

            var tarea1 = new TareaRequestDto
            {
                Descripcion = "Test tarea #1",
                FechaTarea = new DateOnly(2025, 10, 1),
                Visibilidad = 1,
                Estado = 1,
                EstimacionId = 2,
                Completado = true
            };

            listDtos.Add(tarea1);

            var tarea2 = new TareaRequestDto
            {
                Descripcion = "Test tarea #2",
                FechaTarea = new DateOnly(2025, 10, 2),
                Visibilidad = 2,
                Estado = 2,
                EstimacionId = 5,
                Completado = false
            };

            listDtos.Add(tarea2);

            var tareas = mapper.Map<List<Tarea>>(listDtos);

            Assert.Equal(listDtos[0].Descripcion, tareas[0].Descripcion);
            Assert.Equal(listDtos[0].FechaTarea, tareas[0].FechaTarea);
            Assert.Equal(listDtos[0].Visibilidad, tareas[0].Visibilidad);
            Assert.Equal(listDtos[0].Estado, tareas[0].Estado);
            Assert.Equal(listDtos[0].EstimacionId, tareas[0].EstimacionId);
            Assert.Equal(listDtos[0].Completado, tareas[0].Completado);

            Assert.Equal(listDtos[1].Descripcion, tareas[1].Descripcion);
            Assert.Equal(listDtos[1].FechaTarea, tareas[1].FechaTarea);
            Assert.Equal(listDtos[1].Visibilidad, tareas[1].Visibilidad);
            Assert.Equal(listDtos[1].Estado, tareas[1].Estado);
            Assert.Equal(listDtos[1].EstimacionId, tareas[1].EstimacionId);
            Assert.Equal(listDtos[1].Completado, tareas[1].Completado);
        }
    }
}