using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class mig_Add_ProductGenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductGenderId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductGender",
                columns: table => new
                {
                    ProductGenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductGenderTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGender", x => x.ProductGenderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGenderId",
                table: "Products",
                column: "ProductGenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGender_ProductGenderId",
                table: "Products",
                column: "ProductGenderId",
                principalTable: "ProductGender",
                principalColumn: "ProductGenderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGender_ProductGenderId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductGender");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductGenderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductGenderId",
                table: "Products");
        }
    }
}
