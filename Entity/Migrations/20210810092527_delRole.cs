using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class delRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21a22fe7-957b-40c4-9706-92664e1db185");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80ee005d-f3ce-4d8c-8ec8-b89b188411fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e12e0325-54af-45c6-a14e-baf9013b006f");

            migrationBuilder.UpdateData(
                table: "Lots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e12e0325-54af-45c6-a14e-baf9013b006f", "81c3f056-5cf0-4807-9a5f-8e7e26f0e199", "Seller", "SELLER" },
                    { "21a22fe7-957b-40c4-9706-92664e1db185", "5d52bbf2-bcda-4f11-bae6-27057f3c09b4", "Buyer", "BUYER" },
                    { "80ee005d-f3ce-4d8c-8ec8-b89b188411fd", "1e72f7ad-b102-4284-a466-c2bae8a6f708", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Lots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2021, 8, 16, 11, 23, 0, 831, DateTimeKind.Local).AddTicks(8046), new DateTime(2021, 8, 9, 11, 23, 0, 830, DateTimeKind.Local).AddTicks(7445) });
        }
    }
}
