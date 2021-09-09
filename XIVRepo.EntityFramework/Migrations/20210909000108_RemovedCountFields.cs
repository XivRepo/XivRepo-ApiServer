using Microsoft.EntityFrameworkCore.Migrations;

namespace XIVRepo.EntityFramework.Migrations
{
    public partial class RemovedCountFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDownloads",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "TotalFollows",
                table: "Mods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalDownloads",
                table: "Mods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalFollows",
                table: "Mods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
