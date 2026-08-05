using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigrations
{
    /// <inheritdoc />
    public partial class ModifiedTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedHours",
                table: "Task",
                newName: "TaskHours");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPull",
                table: "Task",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LastUsedHours",
                table: "Task",
                type: "decimal(6,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingHours",
                table: "Task",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPull",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "LastUsedHours",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "RemainingHours",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "TaskHours",
                table: "Task",
                newName: "AssignedHours");
        }
    }
}
