using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintTicketAPI.Migrations
{
    public partial class keychange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Organizations",
                newName: "orgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "orgId",
                table: "Organizations",
                newName: "Id");
        }
    }
}
