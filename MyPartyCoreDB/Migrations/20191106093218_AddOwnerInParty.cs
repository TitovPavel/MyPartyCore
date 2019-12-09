using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPartyCore.DB.Migrations
{
    public partial class AddOwnerInParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Parties",
                nullable: true);

            migrationBuilder.Sql(
            @"
                UPDATE Parties
                SET Parties.OwnerId = (SELECT TOP (1)(AspNetUsers.Id) FROM AspNetUsers) ;
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_OwnerId",
                table: "Parties",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_AspNetUsers_OwnerId",
                table: "Parties",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_AspNetUsers_OwnerId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_OwnerId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Parties");
        }
    }
}
