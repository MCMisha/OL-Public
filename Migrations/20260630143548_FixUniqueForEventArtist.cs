using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class FixUniqueForEventArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_event_artist_EventId_ArtistId",
                table: "event_artist");

            migrationBuilder.CreateIndex(
                name: "IX_event_artist_EventId",
                table: "event_artist",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_event_artist_EventId",
                table: "event_artist");

            migrationBuilder.CreateIndex(
                name: "IX_event_artist_EventId_ArtistId",
                table: "event_artist",
                columns: new[] { "EventId", "ArtistId" },
                unique: true);
        }
    }
}
