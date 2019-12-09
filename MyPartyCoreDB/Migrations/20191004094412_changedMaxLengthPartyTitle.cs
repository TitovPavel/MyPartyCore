using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPartyCore.DB.Migrations
{
    public partial class changedMaxLengthPartyTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Parties",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Parties",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024);
        }
    }
}
