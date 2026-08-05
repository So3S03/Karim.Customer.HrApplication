using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Migrations.HRMSMigrations
{
    /// <inheritdoc />
    public partial class AddedNewColumnsToFingerprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Long",
                table: "Fingerprint",
                newName: "CheckInLong");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "Fingerprint",
                newName: "CheckInLat");

            migrationBuilder.AddColumn<decimal>(
                name: "CheckOutLat",
                table: "Fingerprint",
                type: "decimal(10,7)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CheckOutLong",
                table: "Fingerprint",
                type: "decimal(10,7)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckOutLat",
                table: "Fingerprint");

            migrationBuilder.DropColumn(
                name: "CheckOutLong",
                table: "Fingerprint");

            migrationBuilder.RenameColumn(
                name: "CheckInLong",
                table: "Fingerprint",
                newName: "Long");

            migrationBuilder.RenameColumn(
                name: "CheckInLat",
                table: "Fingerprint",
                newName: "Lat");
        }
    }
}
