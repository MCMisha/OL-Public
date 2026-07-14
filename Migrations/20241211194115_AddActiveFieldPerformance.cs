using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveFieldPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "performance",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "performance");
        }
    }
}
