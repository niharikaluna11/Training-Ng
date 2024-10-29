using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplyForClaimApplication.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    DateOfIncident = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimType = table.Column<int>(type: "int", nullable: false),
                    ClaimantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimantPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimantEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_Claims_ClaimTypes_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "ClaimTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsuredName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyNumber);
                    table.ForeignKey(
                        name: "FK_Policies_ClaimTypes_PolicyType",
                        column: x => x.PolicyType,
                        principalTable: "ClaimTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    Settlementform = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DeathCertificate = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PolicyCertificate = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AddressProof = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CancelledCheck = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Others = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ClaimId",
                table: "Documents",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PolicyType",
                table: "Policies",
                column: "PolicyType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ClaimTypes");
        }
    }
}
