using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class mig_AddValueIn_ProductStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductStatuses",
                columns: new[] { "StatusId", "StatusTitel" },
                values: new object[] { 1, "موجود" });

            migrationBuilder.InsertData(
                table: "ProductStatuses",
                columns: new[] { "StatusId", "StatusTitel" },
                values: new object[] { 2, "نا موجود" });

            migrationBuilder.InsertData(
                table: "ProductStatuses",
                columns: new[] { "StatusId", "StatusTitel" },
                values: new object[] { 3, "به زودی" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductStatuses",
                keyColumn: "StatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductStatuses",
                keyColumn: "StatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductStatuses",
                keyColumn: "StatusId",
                keyValue: 3);
        }
    }
}
