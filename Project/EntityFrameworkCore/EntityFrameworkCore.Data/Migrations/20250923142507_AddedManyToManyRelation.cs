using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedManyToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwayTeamScore",
                table: "Match",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamScore",
                table: "Match",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Match_AwayTeamId",
                table: "Match",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_HomeTeamId",
                table: "Match",
                column: "HomeTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Team_AwayTeamId",
                table: "Match",
                column: "AwayTeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Team_HomeTeamId",
                table: "Match",
                column: "HomeTeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_AwayTeamId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_HomeTeamId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_AwayTeamId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_HomeTeamId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "AwayTeamScore",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "HomeTeamScore",
                table: "Match");
        }
    }
}
