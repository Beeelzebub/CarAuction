using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b655e40-ee15-43c4-8535-0c89ccd91f21");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2dfe30a-8e28-4955-82a0-f3c5096ef641", "00b49307-e486-4828-9fec-8801b83500a9", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2dfe30a-8e28-4955-82a0-f3c5096ef641");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9b655e40-ee15-43c4-8535-0c89ccd91f21", "32a50027-24c6-42c4-bf23-7cf2613b945e", "Admin", "ADMIN" });
        }
    }
}
