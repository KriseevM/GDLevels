using Microsoft.EntityFrameworkCore.Migrations;

namespace GDLevels.Migrations
{
    public partial class DefaultDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestTime = table.Column<int>(type: "INTEGER", nullable: false),
                    LevelID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LevelID",
                table: "Levels",
                column: "LevelID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}
