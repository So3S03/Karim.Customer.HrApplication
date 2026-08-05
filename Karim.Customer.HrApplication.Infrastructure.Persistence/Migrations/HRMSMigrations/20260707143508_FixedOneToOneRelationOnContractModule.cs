using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigrations
{
    /// <inheritdoc />
    public partial class FixedOneToOneRelationOnContractModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Contract_ContractId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Contract_ContractId",
                table: "Project");

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

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Contract",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmpId",
                table: "Contract",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_EmpId",
                table: "Contract",
                column: "EmpId",
                unique: true,
                filter: "[EmpId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProjectId",
                table: "Contract",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Employee_EmpId",
                table: "Contract",
                column: "EmpId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Project_ProjectId",
                table: "Contract",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Employee_EmpId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Project_ProjectId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_EmpId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_ProjectId",
                table: "Contract");

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

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmpId",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
