using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    desc = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    lang = table.Column<string>(type: "TEXT", nullable: false),
                    isbn = table.Column<string>(type: "TEXT", nullable: false),
                    datePub = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<int>(type: "INTEGER", nullable: false),
                    author = table.Column<string>(type: "TEXT", nullable: false),
                    urlImg = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
