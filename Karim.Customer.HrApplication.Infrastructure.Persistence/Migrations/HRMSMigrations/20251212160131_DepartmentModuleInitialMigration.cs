using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigrations
{
    /// <inheritdoc />
    public partial class DepartmentModuleInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isRemoved = table.Column<bool>(type: "bit", nullable: false),
                    ActualCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDepartmentBudget = table.Column<decimal>(type: "decimal(22,2)", precision: 22, scale: 2, nullable: false),
                    DepartmentBudgetForSalaries = table.Column<decimal>(type: "decimal(22,2)", precision: 22, scale: 2, nullable: false),
                    DepartmentBudgetForTools = table.Column<decimal>(type: "decimal(22,2)", precision: 22, scale: 2, nullable: true),
                    DepartmentBudgetForTrainees = table.Column<decimal>(type: "decimal(22,2)", precision: 22, scale: 2, nullable: true),
                    DepartmentBudgetOther = table.Column<decimal>(type: "decimal(22,2)", precision: 22, scale: 2, nullable: true),
                    DepatrmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
