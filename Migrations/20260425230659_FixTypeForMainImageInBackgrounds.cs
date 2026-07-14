using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class FixTypeForMainImageInBackgrounds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     ALTER TABLE main_page_background
                                     ALTER COLUMN "MainImage" TYPE bytea
                                     USING decode("MainImage", 'base64');
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     ALTER TABLE main_page_background
                                     ALTER COLUMN "MainImage" TYPE bytea
                                     USING decode("MainImage", 'base64');
                                 """);
        }
    }
}
