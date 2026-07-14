using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class DeleteContentTypeFieldFromBackground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "main_page_background");

            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "main_page_background",
                newName: "MainImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainImage",
                table: "main_page_background",
                newName: "ImageBase64");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "main_page_background",
                type: "text",
                nullable: true);
        }
    }
}
