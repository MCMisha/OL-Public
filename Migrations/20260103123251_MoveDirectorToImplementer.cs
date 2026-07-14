using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class MoveDirectorToImplementer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "performance_director");

            migrationBuilder.DropTable(
                name: "director");

            migrationBuilder.AddColumn<bool>(
                name: "IsDirector",
                table: "implementer",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDirector",
                table: "implementer");

            migrationBuilder.CreateTable(
                name: "director",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_director", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "performance_director",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DirectorId = table.Column<int>(type: "integer", nullable: false),
                    PerformanceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceDirectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceDirectors_director_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "director",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformanceDirectors_performance_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "performance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceDirectors_DirectorId",
                table: "performance_director",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceDirectors_PerformanceId",
                table: "performance_director",
                column: "PerformanceId");
        }
    }
}
