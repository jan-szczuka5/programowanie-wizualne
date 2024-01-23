using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _148075._148159.PhonesCatalog.Web.Migrations
{
    /// <inheritdoc />
    public partial class WebMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Phones",
                newName: "ProducerID");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ProducerID",
                table: "Phones",
                column: "ProducerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Producers_ProducerID",
                table: "Phones",
                column: "ProducerID",
                principalTable: "Producers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Producers_ProducerID",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_ProducerID",
                table: "Phones");

            migrationBuilder.RenameColumn(
                name: "ProducerID",
                table: "Phones",
                newName: "ProducerId");
        }
    }
}
