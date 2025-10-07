using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using CE_TaskTest.TestSector.E2ETests.Resources;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace CE_TaskTest.TestSector.E2ETests;

public class TareaControllerE2ETests : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task Get_ShouldReturnList()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/Tarea");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tareas = await response.Content.ReadFromJsonAsync<List<TareaResponseDto>>();

        tareas.Should().NotBeNull();
        tareas.Should().HaveCount(10);
        tareas![0].Descripcion.Should().Be("Diseñar la arquitectura del sistema");
    }

    [Fact]
    public async Task GetAll_ShouldReturn404_NoTareas()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CE_TaskTest.BackEnd.Context.ApplicationDbContext>();
        db.Tareas.RemoveRange(db.Tareas);
        db.SaveChanges();

        var response = await client.GetAsync("/api/Task");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ShouldReturnTarea()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/tarea/8");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tarea = await response.Content.ReadFromJsonAsync<TareaResponseDto>();

        tarea.Should().NotBeNull();
        tarea!.Descripcion.Should().Be("Crear documentación de API con Swagger");
        tarea.Estado.Should().Be(2);
    }

    [Fact]
    public async Task GetById_ShoulReturn404()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/tarea/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCompleted_ShouldReturnAllCompletedTareas()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/Tarea/Completed");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tareas = await response.Content.ReadFromJsonAsync<List<TareaResponseDto>>();
        tareas.Should().OnlyContain(t => t.Completado);
    }

    [Fact]
    public async Task Post_ShouldCreateNewTarea()
    {
        var request = new TareaRequestDto
        {
            Descripcion = "Nueva tarea de prueba",
            Estado = (int)EEstado.Normal,
            Completado = false,
            FechaTarea = DateOnly.FromDateTime(DateTime.Now),
            EstimacionId = 3,
            Visibilidad = (int)EVisibility.Publico
        };

        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/Tarea", request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Put_ShouldUpdateExistingTarea()
    {
        var request = new TareaRequestDto
        {
            Descripcion = "Tarea actualizada",
            Estado = 1,
            Completado = false,
            FechaTarea = DateOnly.FromDateTime(DateTime.Now)
        };

        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var response = await client.PutAsJsonAsync("/api/Tarea/1", request);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Patch_ShouldSwitchCompletedState()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var response = await client.PatchAsync("/api/Tarea/UpdateAchievedState?id=1", null);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var tarea = await db.Tareas.FindAsync(1);
        tarea!.Completado.Should().BeFalse();
    }

    [Fact]
    public async Task GetCompleted_ShouldReturn404IFNoCompleted()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Tareas.ToList().ForEach(t => t.Completado = false);
        db.SaveChanges();

        var response = await client.GetAsync("/api/Tarea/Completed");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
