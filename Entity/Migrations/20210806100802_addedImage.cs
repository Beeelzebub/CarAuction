using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class addedImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6eb2a80b-268d-47a6-b34e-eebd208abfd6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac704f4-1790-4945-9be5-c20ef08052c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4f3ec83-c20b-4cc4-af70-4be52839cb36");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cars",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "83cea89b-3283-43a8-b928-8d53b92ac8ea", "d52f525b-40bc-4b7d-9bba-f857a0ea00ef", "Seller", "SELLER" },
                    { "99fbba00-2725-4da5-aa9d-2ce9b5211706", "762edc6e-37c2-4a1a-bd52-c89e615cfe17", "Buyer", "BUYER" },
                    { "62076629-43c4-4e2d-9870-b1b5a18a7907", "fb2aeddb-b880-405d-b820-50cf141bf64b", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("67645961-17a7-4316-853c-7ea15838c135"),
                column: "ImageUrl",
                value: "https://americamotorsby.ams3.digitaloceanspaces.com/2269/38169871_Image_1.JPG");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62076629-43c4-4e2d-9870-b1b5a18a7907");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83cea89b-3283-43a8-b928-8d53b92ac8ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99fbba00-2725-4da5-aa9d-2ce9b5211706");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4f3ec83-c20b-4cc4-af70-4be52839cb36", "2488386d-d4d1-454b-851f-832fab44acb7", "Seller", "SELLER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cac704f4-1790-4945-9be5-c20ef08052c5", "d43680f0-c85e-40a4-97f5-b9d7f3f30c95", "Buyer", "BUYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6eb2a80b-268d-47a6-b34e-eebd208abfd6", "582e7de1-ece4-4b2a-93c5-8a25d8e1af6c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
