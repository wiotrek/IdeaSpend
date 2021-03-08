using Microsoft.EntityFrameworkCore.Migrations;

namespace IdeaSpend.API.Migrations
{
    public partial class CascadeRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Platnosci_ProductId",
                table: "Platnosci");

            migrationBuilder.CreateIndex(
                name: "IX_Platnosci_ProductId",
                table: "Platnosci",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Platnosci_ProductId",
                table: "Platnosci");

            migrationBuilder.CreateIndex(
                name: "IX_Platnosci_ProductId",
                table: "Platnosci",
                column: "ProductId");
        }
    }
}
