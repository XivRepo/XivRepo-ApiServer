using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XIVRepo.EntityFramework.Migrations
{
    public partial class ModDependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModDependencies",
                columns: table => new
                {
                    BaseModId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ModDependencyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModDependencies", x => new { x.BaseModId, x.ModDependencyId });
                    table.ForeignKey(
                        name: "FK_ModDependencies_Mods_BaseModId",
                        column: x => x.BaseModId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModDependencies_Mods_ModDependencyId",
                        column: x => x.ModDependencyId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ModDependencies_ModDependencyId",
                table: "ModDependencies",
                column: "ModDependencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModDependencies");
        }
    }
}
