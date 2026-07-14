    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    #nullable disable

    namespace WebApplicationOperaLublin.Migrations
    {
        /// <inheritdoc />
        public partial class PlaceInformationMigration : Migration
        {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateSequence<int>(
                    name: "performance_id_seq");

                migrationBuilder.CreateSequence<int>(
                    name: "user_id_seq");

                migrationBuilder.CreateTable(
                    name: "genre",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("genre_pk", x => x.id);
                    });

                migrationBuilder.CreateTable(
                    name: "place",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("place_id_pk", x => x.id);
                    });
                migrationBuilder.CreateTable(
                    name: "user",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                        password_hash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("user_id_pk", x => x.id);
                    });

                migrationBuilder.CreateTable(
                    name: "performance",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                        genre = table.Column<int>(type: "integer", nullable: false),
                        place = table.Column<int>(type: "integer", nullable: false),
                        duration = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                        breaks_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                        description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                        main_image = table.Column<byte[]>(type: "bytea", nullable: false),
                        poster = table.Column<byte[]>(type: "bytea", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("perfomances_id_pk", x => x.id);
                        table.ForeignKey(
                            name: "perfomance_genre_id_fk",
                            column: x => x.genre,
                            principalTable: "genre",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "perfomance_place_id_fk",
                            column: x => x.place,
                            principalTable: "place",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_performance_genre",
                    table: "performance",
                    column: "genre");

                migrationBuilder.CreateIndex(
                    name: "IX_performance_place",
                    table: "performance",
                    column: "place");
            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropTable(
                    name: "performance");

                migrationBuilder.DropTable(
                    name: "user");

                migrationBuilder.DropTable(
                    name: "genre");

                migrationBuilder.DropTable(
                    name: "place");

                migrationBuilder.DropSequence(
                    name: "performance_id_seq");

                migrationBuilder.DropSequence(
                    name: "user_id_seq");
            }
        }
    }
