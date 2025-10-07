using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTest.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estimaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaTarea = table.Column<DateOnly>(type: "date", nullable: false),
                    Visibilidad = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    EstimacionId = table.Column<int>(type: "integer", nullable: false),
                    Completado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Estimaciones_EstimacionId",
                        column: x => x.EstimacionId,
                        principalTable: "Estimaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estimaciones",
                columns: new[] { "Id", "Activo", "Duration" },
                values: new object[,]
                {
                    { 1, true, 5 },
                    { 2, true, 10 },
                    { 3, true, 15 },
                    { 4, true, 20 },
                    { 5, true, 25 },
                    { 6, true, 30 },
                    { 7, true, 40 },
                    { 8, true, 50 }
                });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "Completado", "Descripcion", "Estado", "EstimacionId", "FechaTarea", "Visibilidad" },
                values: new object[,]
                {
                    { 1, true, "Diseñar la arquitectura del sistema", 1, 8, new DateOnly(2025, 10, 1), 1 },
                    { 2, true, "Implementar autenticación JWT", 0, 5, new DateOnly(2025, 10, 2), 2 },
                    { 3, true, "Configurar CI/CD en GitHub Actions", 2, 3, new DateOnly(2025, 10, 3), 1 },
                    { 4, false, "Crear pruebas unitarias para UserService", 1, 4, new DateOnly(2025, 10, 4), 2 },
                    { 5, true, "Optimizar consultas con EF Core", 0, 6, new DateOnly(2025, 10, 5), 1 },
                    { 6, false, "Integrar pagos con Stripe", 2, 7, new DateOnly(2025, 10, 6), 2 },
                    { 7, false, "Revisión de seguridad OWASP", 1, 5, new DateOnly(2025, 10, 7), 1 },
                    { 8, false, "Crear documentación de API con Swagger", 2, 2, new DateOnly(2025, 10, 8), 1 },
                    { 9, false, "Implementar notificaciones en tiempo real", 0, 6, new DateOnly(2025, 10, 9), 2 },
                    { 10, true, "Refactorizar código legacy", 1, 4, new DateOnly(2025, 10, 10), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_EstimacionId",
                table: "Tareas",
                column: "EstimacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Estimaciones");
        }
    }
}
