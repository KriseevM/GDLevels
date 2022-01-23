using Microsoft.EntityFrameworkCore.Migrations;

namespace GDLevels.Migrations
{
    public partial class IsUnique_to_AlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Levels_LevelID",
                table: "Levels");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Levels_LevelID",
                table: "Levels",
                column: "LevelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Levels_LevelID",
                table: "Levels");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LevelID",
                table: "Levels",
                column: "LevelID",
                unique: true);
        }
    }
}
