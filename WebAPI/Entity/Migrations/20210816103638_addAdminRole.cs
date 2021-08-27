using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class addAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04473cda-e868-4228-a415-97f0cc98d2ba", "96b4e583-f7ef-40d0-a2c0-0f241bf525ce", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04473cda-e868-4228-a415-97f0cc98d2ba");
        }
    }
}
