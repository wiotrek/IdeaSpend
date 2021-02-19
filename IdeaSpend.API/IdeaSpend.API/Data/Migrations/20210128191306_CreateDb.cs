using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdeaSpend.API.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    imie = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    nazwisko = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    nazwa_uzytkownika = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    salt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    hash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    account_created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_login = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Platnosci",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    sprzedawca = table.Column<string>(type: "TEXT", nullable: true),
                    nazwa_produktu = table.Column<string>(type: "TEXT", nullable: true),
                    cena = table.Column<double>(type: "REAL", nullable: false),
                    ilosc = table.Column<int>(type: "INTEGER", nullable: false),
                    kategoria = table.Column<string>(type: "TEXT", nullable: true),
                    waga = table.Column<double>(type: "REAL", nullable: false),
                    jednostka = table.Column<string>(type: "TEXT", nullable: true),
                    data_transakcji = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platnosci", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Platnosci_Uzytkownik_UserId",
                        column: x => x.UserId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platnosci_UserId",
                table: "Platnosci",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platnosci");

            migrationBuilder.DropTable(
                name: "Uzytkownik");
        }
    }
}
