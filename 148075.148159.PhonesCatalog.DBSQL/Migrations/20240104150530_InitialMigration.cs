using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _148075._148159.PhonesCatalog.DBSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhonesRelation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProducerID = table.Column<int>(type: "INTEGER", nullable: false),
                    YearOfProduction = table.Column<int>(type: "INTEGER", nullable: false),
                    AlreadySold = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    SoftwareType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhonesRelation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProducersRelation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducersRelation", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhonesRelation");

            migrationBuilder.DropTable(
                name: "ProducersRelation");
        }
    }
}
