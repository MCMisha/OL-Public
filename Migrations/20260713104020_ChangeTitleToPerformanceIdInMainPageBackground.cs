using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTitleToPerformanceIdInMainPageBackground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "main_page_background");

            migrationBuilder.AddColumn<int>(
                name: "PerformanceId",
                table: "main_page_background",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_main_page_background_PerformanceId",
                table: "main_page_background",
                column: "PerformanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_main_page_background_performance_PerformanceId",
                table: "main_page_background",
                column: "PerformanceId",
                principalTable: "performance",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_main_page_background_performance_PerformanceId",
                table: "main_page_background");

            migrationBuilder.DropIndex(
                name: "IX_main_page_background_PerformanceId",
                table: "main_page_background");

            migrationBuilder.DropColumn(
                name: "PerformanceId",
                table: "main_page_background");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "main_page_background",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
