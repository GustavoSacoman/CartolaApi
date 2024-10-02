using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartolaApi.Migrations
{
    /// <inheritdoc />
    public partial class Cartola005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Seasons_SeasonId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_SeasonId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Id",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Tournaments");

            migrationBuilder.AddColumn<int>(
                name: "TournamentsId",
                table: "Seasons",
                type: "int",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentsId",
                table: "Seasons");

            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SeasonId",
                table: "Tournaments",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Id",
                table: "Teams",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Seasons_SeasonId",
                table: "Tournaments",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }
    }
}
