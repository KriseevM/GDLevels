using Microsoft.EntityFrameworkCore.Migrations;

namespace GDLevels.Migrations
{
    public partial class ID_to_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Levels",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Levels",
                newName: "ID");
        }
    }
}
