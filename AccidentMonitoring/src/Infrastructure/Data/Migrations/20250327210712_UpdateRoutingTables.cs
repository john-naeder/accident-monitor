using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccidentMonitoring.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoutingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccidentVehicles",
                table: "AccidentVehicles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccidentVehicles",
                table: "AccidentVehicles",
                columns: new[] { "AccidentId", "VehicleId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccidentVehicles",
                table: "AccidentVehicles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccidentVehicles",
                table: "AccidentVehicles",
                columns: new[] { "AccidentId", "VehicleId", "Id" });
        }
    }
}
