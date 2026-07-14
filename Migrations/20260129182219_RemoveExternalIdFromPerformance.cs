using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExternalIdFromPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "performance");

            migrationBuilder.CreateIndex(
                name: "IX_performance_event_StartAt",
                table: "performance_event",
                column: "StartAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_performance_event_StartAt",
                table: "performance_event");

            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "performance",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
