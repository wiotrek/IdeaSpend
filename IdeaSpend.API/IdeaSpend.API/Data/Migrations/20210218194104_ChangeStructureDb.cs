using Microsoft.EntityFrameworkCore.Migrations;

namespace IdeaSpend.API.Migrations
{
    public partial class ChangeStructureDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "waga",
                table: "Produkty");

            migrationBuilder.RenameColumn(
                name: "last_login",
                table: "Uzytkownik",
                newName: "ostatnie_logowanie");

            migrationBuilder.RenameColumn(
                name: "account_created",
                table: "Uzytkownik",
                newName: "utworzono");

            migrationBuilder.AddColumn<double>(
                name: "dochód",
                table: "Uzytkownik",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "waga",
                table: "Platnosci",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "waluta",
                table: "Platnosci",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dochód",
                table: "Uzytkownik");

            migrationBuilder.DropColumn(
                name: "waga",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "waluta",
                table: "Platnosci");

            migrationBuilder.RenameColumn(
                name: "utworzono",
                table: "Uzytkownik",
                newName: "account_created");

            migrationBuilder.RenameColumn(
                name: "ostatnie_logowanie",
                table: "Uzytkownik",
                newName: "last_login");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Produkty",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "waga",
                table: "Produkty",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
