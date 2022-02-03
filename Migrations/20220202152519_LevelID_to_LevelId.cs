using Microsoft.EntityFrameworkCore.Migrations;

namespace GDLevels.Migrations
{
    public partial class LevelID_to_LevelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Levels_LevelID",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "LevelID",
                table: "Levels",
                newName: "LevelId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Levels_LevelId",
                table: "Levels",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Levels_LevelId",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Levels",
                newName: "LevelID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Levels_LevelID",
                table: "Levels",
                column: "LevelID");
        }
    }
}
