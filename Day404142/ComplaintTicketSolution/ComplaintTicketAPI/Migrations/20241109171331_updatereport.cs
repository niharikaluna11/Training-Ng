using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComplaintTicketAPI.Migrations
{
    public partial class updatereport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "ComplaintReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintReports_OrganizationId",
                table: "ComplaintReports",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintReports_Organizations_OrganizationId",
                table: "ComplaintReports",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintReports_Organizations_OrganizationId",
                table: "ComplaintReports");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintReports_OrganizationId",
                table: "ComplaintReports");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "ComplaintReports");
        }
    }
}
