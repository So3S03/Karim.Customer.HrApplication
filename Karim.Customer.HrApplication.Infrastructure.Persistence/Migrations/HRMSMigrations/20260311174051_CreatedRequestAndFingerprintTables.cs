using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigrations
{
    /// <inheritdoc />
    public partial class CreatedRequestAndFingerprintTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_AccountId",
                table: "Employee");

            migrationBuilder.CreateTable(
                name: "Fingerprint",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CheckIn = table.Column<TimeOnly>(type: "time", nullable: false),
                    CheckOut = table.Column<TimeOnly>(type: "time", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DurationInHours = table.Column<int>(type: "int", nullable: true),
                    Long = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fingerprint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fingerprint_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fingerprint_EmpId",
                table: "Fingerprint",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EmpId",
                table: "Requests",
                column: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_AccountId",
                table: "Employee",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_AccountId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Fingerprint");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_AccountId",
                table: "Employee",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
