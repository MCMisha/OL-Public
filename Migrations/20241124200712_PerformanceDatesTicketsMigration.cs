using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class PerformanceDatesTicketsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence("performance_dates_tickets_seq");
            migrationBuilder.CreateTable(
                name: "performance_dates_tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PerformanceId = table.Column<int>(type: "integer", nullable: false),
                    DateTimePerformance = table.Column<DateTime>(type: "timestamp", nullable: false),
                    TicketsCount = table.Column<int>(type: "integer", nullable: false),
                    TicketsSold = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceDirectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceDatesPerformance",
                        column: x => x.PerformanceId,
                        principalTable: "performance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceDatesTickets");
            migrationBuilder.DropSequence("performance_dates_tickets_seq");
        }
    }
}
