using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class FixDescriptionLengthForArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "artist",
                type: "character varying(6000)",
                maxLength: 6000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5000)",
                oldMaxLength: 5000,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "artist",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(6000)",
                oldMaxLength: 6000,
                oldNullable: true);
        }
    }
}
