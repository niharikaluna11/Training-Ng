using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFFirstAPI.Migrations
{
    public partial class variableadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WishList",
                table: "Carts",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WishList",
                table: "Carts");
        }
    }
}
