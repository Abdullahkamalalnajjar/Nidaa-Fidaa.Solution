using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nidaa_Fidaa.Respository.Data.Migrations
{
    public partial class UpdateBasket3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Baskets",
                newName: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Baskets",
                newName: "UserId");
        }
    }
}
