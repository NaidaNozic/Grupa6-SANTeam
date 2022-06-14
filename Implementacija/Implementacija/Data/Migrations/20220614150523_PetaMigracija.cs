using Microsoft.EntityFrameworkCore.Migrations;

namespace Implementacija.Data.Migrations
{
    public partial class PetaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Osoba_osobaId",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Osoba_osobaId",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Odgovori_Komentar_KomentarId",
                table: "Odgovori");

            migrationBuilder.DropForeignKey(
                name: "FK_Zaposlenik_Osoba_osobaId",
                table: "Zaposlenik");

            migrationBuilder.AlterColumn<int>(
                name: "osobaId",
                table: "Zaposlenik",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "osobaId",
                table: "Korisnik",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "osobaId",
                table: "Administrator",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kolekcija_KorisnikId",
                table: "Kolekcija",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Osoba_osobaId",
                table: "Administrator",
                column: "osobaId",
                principalTable: "Osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Osoba_osobaId",
                table: "Korisnik",
                column: "osobaId",
                principalTable: "Osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zaposlenik_Osoba_osobaId",
                table: "Zaposlenik",
                column: "osobaId",
                principalTable: "Osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Osoba_osobaId",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Kolekcija_Korisnik_KorisnikId",
                table: "Kolekcija");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Osoba_osobaId",
                table: "Korisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Zaposlenik_Osoba_osobaId",
                table: "Zaposlenik");

            migrationBuilder.DropIndex(
                name: "IX_Kolekcija_KorisnikId",
                table: "Kolekcija");

            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Odgovori");

            migrationBuilder.DropColumn(
                name: "Tekst",
                table: "Odgovori");

            migrationBuilder.DropColumn(
                name: "movieId",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "tmbd_id",
                table: "Komentar");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Kolekcija");

            migrationBuilder.DropColumn(
                name: "tmbd_id",
                table: "Film");

            migrationBuilder.AlterColumn<int>(
                name: "osobaId",
                table: "Zaposlenik",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Odgovori",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "osobaId",
                table: "Korisnik",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "osobaId",
                table: "Administrator",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Odgovori_FilmId",
                table: "Odgovori",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Odgovori_KomentarId",
                table: "Odgovori",
                column: "KomentarId");

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
                name: "FK_Odgovori_Film_FilmId",
                table: "Odgovori",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Odgovori_Komentar_KomentarId",
                table: "Odgovori",
                column: "KomentarId",
                principalTable: "Komentar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zaposlenik_Osoba_osobaId",
                table: "Zaposlenik",
                column: "osobaId",
                principalTable: "Osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
