using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigration
{
    /// <inheritdoc />
    public partial class RemovedDurationAddFingerprintColumnsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "FingerprintId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "Fingerprint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_FingerprintId",
                table: "Requests",
                column: "FingerprintId",
                unique: true,
                filter: "[FingerprintId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Fingerprint_FingerprintId",
                table: "Requests",
                column: "FingerprintId",
                principalTable: "Fingerprint",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Fingerprint_FingerprintId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_FingerprintId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "FingerprintId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Fingerprint");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Requests",
                type: "int",
                nullable: true);
        }
    }
}
