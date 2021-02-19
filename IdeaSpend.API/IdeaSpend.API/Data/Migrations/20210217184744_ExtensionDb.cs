using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdeaSpend.API.Migrations
{
    public partial class ExtensionDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cena",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "jednostka",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "kategoria",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "nazwa_produktu",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "sprzedawca",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "waga",
                table: "Platnosci");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Platnosci",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Katalog_produktów",
                columns: table => new
                {
                    CatalogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nazwa = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katalog_produktów", x => x.CatalogId);
                });

            migrationBuilder.CreateTable(
                name: "waluta",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    kod = table.Column<string>(type: "TEXT", nullable: true),
                    wartość = table.Column<double>(type: "REAL", nullable: false),
                    data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_waluta", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nazwa_produktu = table.Column<string>(type: "TEXT", nullable: true),
                    sprzedawca = table.Column<string>(type: "TEXT", nullable: true),
                    cena = table.Column<double>(type: "REAL", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    waga = table.Column<double>(type: "REAL", nullable: false),
                    jednostka = table.Column<string>(type: "TEXT", nullable: true),
                    CatalogId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Produkty_Katalog_produktów_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Katalog_produktów",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produkty_Uzytkownik_UserId",
                        column: x => x.UserId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platnosci_ProductId",
                table: "Platnosci",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_CatalogId",
                table: "Produkty",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_UserId",
                table: "Produkty",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Platnosci_Produkty_ProductId",
                table: "Platnosci",
                column: "ProductId",
                principalTable: "Produkty",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platnosci_Produkty_ProductId",
                table: "Platnosci");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "waluta");

            migrationBuilder.DropTable(
                name: "Katalog_produktów");

            migrationBuilder.DropIndex(
                name: "IX_Platnosci_ProductId",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Platnosci");

            migrationBuilder.AddColumn<double>(
                name: "cena",
                table: "Platnosci",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "jednostka",
                table: "Platnosci",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kategoria",
                table: "Platnosci",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nazwa_produktu",
                table: "Platnosci",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sprzedawca",
                table: "Platnosci",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "waga",
                table: "Platnosci",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
