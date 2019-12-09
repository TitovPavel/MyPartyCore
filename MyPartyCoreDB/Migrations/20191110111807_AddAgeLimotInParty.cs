using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPartyCore.DB.Migrations
{
    public partial class AddAgeLimotInParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AgeLimit",
                table: "Parties",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeLimit",
                table: "Parties");
        }
    }
}
