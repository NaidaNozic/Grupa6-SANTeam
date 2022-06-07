using Microsoft.EntityFrameworkCore.Migrations;

namespace Implementacija.Data.Migrations
{
    public partial class DrugaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "osobaId",
                table: "Zaposlenik",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "osobaId",
                table: "Korisnik",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "osobaId",
                table: "Administrator",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenik_osobaId",
                table: "Zaposlenik",
                column: "osobaId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_osobaId",
                table: "Korisnik",
                column: "osobaId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_osobaId",
                table: "Administrator",
                column: "osobaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Osoba_osobaId",
                table: "Administrator",
                column: "osobaId",
                principalTable: "Osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Osoba_osobaId",
                table: "Korisnik",
                column: "osobaId",
                principalTable: "Osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zaposlenik_Osoba_osobaId",
                table: "Zaposlenik",
                column: "osobaId",
                principalTable: "Osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Osoba_osobaId",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Osoba_osobaId",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Zaposlenik_Osoba_osobaId",
                table: "Zaposlenik");

            migrationBuilder.DropIndex(
                name: "IX_Zaposlenik_osobaId",
                table: "Zaposlenik");

            migrationBuilder.DropIndex(
                name: "IX_Korisnik_osobaId",
                table: "Korisnik");

            migrationBuilder.DropIndex(
                name: "IX_Administrator_osobaId",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "osobaId",
                table: "Zaposlenik");

            migrationBuilder.DropColumn(
                name: "osobaId",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "osobaId",
                table: "Administrator");
        }
    }
}
