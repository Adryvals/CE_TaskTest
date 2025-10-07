using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Estimaciones",
                newName: "Duracion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duracion",
                table: "Estimaciones",
                newName: "Duration");
        }
    }
}
