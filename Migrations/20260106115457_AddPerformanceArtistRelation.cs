using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceArtistRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "performance_artist_id_seq");

            migrationBuilder.CreateTable(
                name: "performance_artist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PerformanceId = table.Column<int>(type: "integer", nullable: false),
                    ArtistId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_performance_artist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_performance_artist_artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "artist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_performance_artist_performance_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "performance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_performance_artist_ArtistId",
                table: "performance_artist",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_performance_artist_PerformanceId_ArtistId",
                table: "performance_artist",
                columns: new[] { "PerformanceId", "ArtistId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "performance_artist");

            migrationBuilder.DropSequence(
                name: "performance_artist_id_seq");
        }
    }
}
