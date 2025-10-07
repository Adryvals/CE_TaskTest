using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services;
using CE_TaskTest.TestSector.MockData;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CE_TaskTest.TestSector.IntegrationTests.Services
{
    public class TareaServiceTests
    {
        [Fact]
        public void GetCompletadas_ReturnsOnlyCompletedTasks()
        {
            var tareas = MockTareas.LoadTareas().AsQueryable();

            var dbSetMock = new Mock<DbSet<Tarea>>();
            dbSetMock.As<IQueryable<Tarea>>().Setup(m => m.Provider).Returns(tareas.Provider);
            dbSetMock.As<IQueryable<Tarea>>().Setup(m => m.Expression).Returns(tareas.Expression);
            dbSetMock.As<IQueryable<Tarea>>().Setup(m => m.ElementType).Returns(tareas.ElementType);
            dbSetMock.As<IQueryable<Tarea>>().Setup(m => m.GetEnumerator()).Returns(tareas.GetEnumerator());

            var contextMock = new Mock<BackEnd.Context.ApplicationDbContext>();
            contextMock.Setup(c => c.Tareas).Returns(dbSetMock.Object);

            var service = new TareaService(contextMock.Object);

            var result = service.GetCompletadas();

            Assert.Contains(result, x => x.Completado);
        }
    }
}