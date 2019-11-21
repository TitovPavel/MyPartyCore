using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPartyCore.DB.Migrations
{
    public partial class AddBaseRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a711a9e-bd2e-4854-91f0-aefb393b2b69", "eefacbf9-7ce8-4efb-8d62-038ba2542fa9", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83b45bb5-f196-4eb0-96ce-77c42272362e", "cf638258-034e-4cf1-8770-cc0eaf516e02", "user", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "3a711a9e-bd2e-4854-91f0-aefb393b2b69", "eefacbf9-7ce8-4efb-8d62-038ba2542fa9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "83b45bb5-f196-4eb0-96ce-77c42272362e", "cf638258-034e-4cf1-8770-cc0eaf516e02" });
        }
    }
}
