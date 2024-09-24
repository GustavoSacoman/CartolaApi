using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartolaApi.Migrations
{
    /// <inheritdoc />
    public partial class Cartola004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    IdMatch = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", maxLength: 50, nullable: false),
                    Result = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdTeam1 = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    IdTeam2 = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    IdTournament = table.Column<int>(type: "int", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.IdMatch);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 255, nullable: true),
                    FinalDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SeasonId",
                table: "Tournaments",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Seasons_SeasonId",
                table: "Tournaments",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Seasons_SeasonId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_SeasonId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Tournaments");
        }
    }
}
