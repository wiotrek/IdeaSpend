using Microsoft.EntityFrameworkCore.Migrations;

namespace IdeaSpend.API.Migrations
{
    public partial class ExtensionTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "zapłacono",
                table: "Platnosci",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "zapłacono",
                table: "Platnosci");
        }
    }
}
