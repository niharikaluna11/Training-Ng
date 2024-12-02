using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintTicketAPI.Migrations
{
    public partial class smallupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Complaints",
                newName: "ComplaintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComplaintId",
                table: "Complaints",
                newName: "Id");
        }
    }
}
