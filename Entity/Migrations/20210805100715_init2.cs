using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06519982-b085-431b-8e1a-ab30d569155e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "346108c7-f0cb-41ba-a836-fa17bb1afc9b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c42aac64-7a17-4510-afa3-d34686141db4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e39cc452-3614-40b3-88bb-1e667394b54b", "e395f67d-e55a-435a-8f15-5b220fb674b9", "Seller", "SELLER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc219595-2f7d-4843-8efc-ac70341921ac", "291ccfdb-5fee-499c-9ce7-331ba80b382c", "Buyer", "BUYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63143a7b-eead-4acc-aeb4-004360ad8247", "2429631e-cd2f-43d5-afb4-316558c1742d", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63143a7b-eead-4acc-aeb4-004360ad8247");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e39cc452-3614-40b3-88bb-1e667394b54b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc219595-2f7d-4843-8efc-ac70341921ac");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "346108c7-f0cb-41ba-a836-fa17bb1afc9b", "c9034bc5-056a-4b37-8224-bf99f75a8daf", "Seller", "SELLER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "06519982-b085-431b-8e1a-ab30d569155e", "aa424785-59ba-4e76-9a43-e2f4458c36d9", "Buyer", "BUYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c42aac64-7a17-4510-afa3-d34686141db4", "62e72b9e-e973-4ac6-b0ca-d612a8d431c8", "Administrator", "ADMINISTRATOR" });
        }
    }
}
