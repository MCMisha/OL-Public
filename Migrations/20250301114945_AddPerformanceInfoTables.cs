using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceInfoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "poster",
                table: "performance",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<byte[]>(
                name: "main_image",
                table: "performance",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
            

            migrationBuilder.CreateTable(
                name: "implementer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_implementer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "performance_implementer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PerformanceId = table.Column<int>(type: "integer", nullable: false),
                    ImplementerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_performance_implementer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_performance_implementer_implementer_ImplementerId",
                        column: x => x.ImplementerId,
                        principalTable: "implementer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_performance_implementer_performance_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "performance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_performance_implementer_ImplementerId",
                table: "performance_implementer",
                column: "ImplementerId");

            migrationBuilder.CreateIndex(
                name: "IX_performance_implementer_PerformanceId",
                table: "performance_implementer",
                column: "PerformanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceDirectors_director_DirectorId",
                table: "performance_director",
                column: "Director",
                principalTable: "director",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceDirectors_director_DirectorId",
                table: "PerformanceDirectors");

            migrationBuilder.DropTable(
                name: "performance_implementer");

            migrationBuilder.DropTable(
                name: "implementer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_director",
                table: "director");

            migrationBuilder.RenameTable(
                name: "director",
                newName: "Directors");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "performance",
                newName: "Active");

            migrationBuilder.AlterColumn<byte[]>(
                name: "poster",
                table: "performance",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "main_image",
                table: "performance",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directors",
                table: "Directors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceDirectors_Directors_DirectorId",
                table: "PerformanceDirectors",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
