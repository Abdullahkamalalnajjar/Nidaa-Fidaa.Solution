using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nidaa_Fidaa.Respository.Data.Migrations
{
    public partial class UpdateNames2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MerchantId",
                table: "Traders",
                newName: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Traders",
                newName: "MerchantId");
        }
    }
}
