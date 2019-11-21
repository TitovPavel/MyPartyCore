using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPartyCore.DB.Migrations
{
    public partial class AddClaimsInDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"
                INSERT INTO [AspNetUserClaims]([UserId], [ClaimType], [ClaimValue]) SELECT [AspNetUsers].[Id], 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth', [AspNetUsers].[Birthday] FROM [AspNetUsers] ;
                INSERT INTO [AspNetUserClaims]([UserId], [ClaimType], [ClaimValue]) SELECT [AspNetUsers].[Id], 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender', [AspNetUsers].[Sex] FROM [AspNetUsers] ;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
