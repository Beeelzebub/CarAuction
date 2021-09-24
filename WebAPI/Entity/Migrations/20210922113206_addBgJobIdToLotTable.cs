using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class addBgJobIdToLotTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d9f7456-7798-4270-a2d3-1895d260dc1f");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundJobId",
                table: "Lots",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91b649e6-9639-4063-bb35-cdc03590e782", "a98a7ba6-25cc-44a3-8509-227062003e24", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91b649e6-9639-4063-bb35-cdc03590e782");

            migrationBuilder.DropColumn(
                name: "BackgroundJobId",
                table: "Lots");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4d9f7456-7798-4270-a2d3-1895d260dc1f", "615b49ac-7438-49b3-b4f7-6e22dd272c23", "Admin", "ADMIN" });
        }
    }
}
