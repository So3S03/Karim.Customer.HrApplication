using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigrations
{
    /// <inheritdoc />
    public partial class CreatedProjectModuleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CanceledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletionPercentage = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    ProjectCoast = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    CoastCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_DepartmentId",
                table: "Project",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
