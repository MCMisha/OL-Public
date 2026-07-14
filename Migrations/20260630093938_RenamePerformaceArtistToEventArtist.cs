using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class RenamePerformaceArtistToEventArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "performance_artist");

            migrationBuilder.DropSequence(
                name: "performance_artist_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "event_artist_id_seq");

            migrationBuilder.CreateTable(
                name: "event_artist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    ArtistId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_artist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_event_artist_artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "artist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_artist_performance_event_EventId",
                        column: x => x.EventId,
                        principalTable: "performance_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_artist_ArtistId",
                table: "event_artist",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_event_artist_EventId_ArtistId",
                table: "event_artist",
                columns: new[] { "EventId", "ArtistId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_artist");

            migrationBuilder.DropSequence(
                name: "event_artist_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "performance_artist_id_seq");

            migrationBuilder.CreateTable(
                name: "performance_artist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistId = table.Column<int>(type: "integer", nullable: false),
                    PerformanceId = table.Column<int>(type: "integer", nullable: false),
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
    }
}
