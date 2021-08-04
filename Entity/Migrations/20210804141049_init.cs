using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17b344e9-ce91-49e8-96e9-0d850a1aa5d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4639fc44-7e9b-4059-a318-a5c6dae32138");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9eee17fb-270a-4806-bde1-1c8632fd9a6b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce389758-b260-47e7-b76e-a03ac0d232f9", "9999a22e-2435-468b-b778-4f7a0f39115a", "Seller", "SELLER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88c0fd8f-2190-472b-a27e-4614885a02f1", "d112f589-0907-46cf-8de7-c40397137bac", "Buyer", "BUYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cde69de2-1520-4304-b568-54100a0a5f1e", "a2b00e5f-ea06-4bd9-aac6-ca240895d118", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88c0fd8f-2190-472b-a27e-4614885a02f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cde69de2-1520-4304-b568-54100a0a5f1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce389758-b260-47e7-b76e-a03ac0d232f9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9eee17fb-270a-4806-bde1-1c8632fd9a6b", "5110eac1-5272-4ff4-9ead-20e7d6e36c77", "Seller", "SELLER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4639fc44-7e9b-4059-a318-a5c6dae32138", "4094fafe-3c6f-4b38-a563-1a8deecb42e8", "Buyer", "BUYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "17b344e9-ce91-49e8-96e9-0d850a1aa5d4", "6d13be65-b466-4818-83ce-091603daa4ef", "Administrator", "ADMINISTRATOR" });
        }
    }
}
