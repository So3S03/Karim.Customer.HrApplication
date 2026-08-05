using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigrations
{
    /// <inheritdoc />
    public partial class CreatedContractEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractId",
                table: "Project",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeerCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyRepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractEmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeWorkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpSalary = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    ContractorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorScopOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractValue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    PaymentTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ContractId",
                table: "Project",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ContractId",
                table: "Employee",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Contract_ContractId",
                table: "Employee",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Contract_ContractId",
                table: "Project",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Contract_ContractId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Contract_ContractId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Project_ContractId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ContractId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Employee");
        }
    }
}
