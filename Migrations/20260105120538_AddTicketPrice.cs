using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ticket_price_group_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "ticket_price_id_seq");

            migrationBuilder.CreateTable(
                name: "ticket_price_groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PerformanceId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPriceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketPriceGroups_performance_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "performance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TicketPriceGroupId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketPrices_TicketPriceGroups_TicketPriceGroupId",
                        column: x => x.TicketPriceGroupId,
                        principalTable: "ticket_price_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketPriceGroups_PerformanceId_SortOrder",
                table: "ticket_price_groups",
                columns: new[] { "PerformanceId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketPrices_TicketPriceGroupId_Type",
                table: "ticket_prices",
                columns: new[] { "TicketPriceGroupId", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_prices");

            migrationBuilder.DropTable(
                name: "ticket_price_groups");

            migrationBuilder.DropSequence(
                name: "ticket_price_group_id_seq");

            migrationBuilder.DropSequence(
                name: "ticket_price_id_seq");
        }
    }
}
