using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations
{
    /// <inheritdoc />
    public partial class GenreBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_genres_GenreID",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_discounts_genres_GenreID",
                table: "discounts");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropIndex(
                name: "IX_discounts_GenreID",
                table: "discounts");

            migrationBuilder.DropIndex(
                name: "IX_books_GenreID",
                table: "books");

            migrationBuilder.DropColumn(
                name: "GenreID",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "GenreID",
                table: "books");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "discounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "books");

            migrationBuilder.AddColumn<int>(
                name: "GenreID",
                table: "discounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreID",
                table: "books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_discounts_GenreID",
                table: "discounts",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_books_GenreID",
                table: "books",
                column: "GenreID");

            migrationBuilder.AddForeignKey(
                name: "FK_books_genres_GenreID",
                table: "books",
                column: "GenreID",
                principalTable: "genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_discounts_genres_GenreID",
                table: "discounts",
                column: "GenreID",
                principalTable: "genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
