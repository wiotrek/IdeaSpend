using Microsoft.EntityFrameworkCore.Migrations;

namespace IdeaSpend.API.Migrations
{
    public partial class ExtensionCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Katalog_produktów",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Katalog_produktów_UserId",
                table: "Katalog_produktów",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Katalog_produktów_Uzytkownik_UserId",
                table: "Katalog_produktów",
                column: "UserId",
                principalTable: "Uzytkownik",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Katalog_produktów_Uzytkownik_UserId",
                table: "Katalog_produktów");

            migrationBuilder.DropIndex(
                name: "IX_Katalog_produktów_UserId",
                table: "Katalog_produktów");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Katalog_produktów");
        }
    }
}
