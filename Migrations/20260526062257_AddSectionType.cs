using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplicationOperaLublin.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "about_section", newName: "section");

            migrationBuilder.RenameSequence(
                name: "about_section_id_seq", newName: "section_id_seq");
            
            migrationBuilder.AddColumn<int>(name: "type", table: "section", nullable: false, defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "type", table: "section");
            
            migrationBuilder.RenameSequence(
                name: "about_section_id_seq", newName: "section_id_seq");
            
            migrationBuilder.RenameTable(
                name: "section", newName: "about_section");
        }
    }
}
