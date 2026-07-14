using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class CreatePerformanceEventTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_TicketPrices_TicketPriceGroupId_Type",
                table: "ticket_prices",
                newName: "IX_ticket_prices_TicketPriceGroupId_Type");

            migrationBuilder.RenameIndex(
                name: "IX_TicketPriceGroups_PerformanceId_SortOrder",
                table: "ticket_price_groups",
                newName: "IX_ticket_price_groups_PerformanceId_SortOrder");

            migrationBuilder.CreateSequence<int>(
                name: "performance_event_id_seq");
            
            migrationBuilder.CreateTable(
                name: "performance_event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PerformanceId = table.Column<int>(type: "integer", nullable: false),
                    StartAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BuyLink = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_performance_event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_performance_event_performance_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "performance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_performance_event_PerformanceId_StartAt",
                table: "performance_event",
                columns: new[] { "PerformanceId", "StartAt" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_price_groups_performance_PerformanceId",
                table: "ticket_price_groups",
                column: "PerformanceId",
                principalTable: "performance",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_prices_ticket_price_groups_TicketPriceGroupId",
                table: "ticket_prices",
                column: "TicketPriceGroupId",
                principalTable: "ticket_price_groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticket_price_groups_performance_PerformanceId",
                table: "ticket_price_groups");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_prices_ticket_price_groups_TicketPriceGroupId",
                table: "ticket_prices");

            migrationBuilder.DropTable(
                name: "performance_event");

            migrationBuilder.DropSequence(
                name: "performance_event_id_seq");

            migrationBuilder.RenameIndex(
                name: "IX_ticket_prices_TicketPriceGroupId_Type",
                table: "TicketPrices",
                newName: "IX_TicketPrices_TicketPriceGroupId_Type");

            migrationBuilder.RenameIndex(
                name: "IX_ticket_price_groups_PerformanceId_SortOrder",
                table: "TicketPriceGroups",
                newName: "IX_TicketPriceGroups_PerformanceId_SortOrder");
        }
    }
}
