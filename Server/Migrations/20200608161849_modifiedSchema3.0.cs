using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class modifiedSchema30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Technicians_TechnicianId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_Repairs_RepairId",
                table: "RepairLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Managers_ManagerId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_ManagerId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Managers_TechnicianId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "Managers");

            migrationBuilder.AlterColumn<long>(
                name: "RepairId",
                table: "RepairLogs",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RepairId",
                table: "Managers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_RepairId",
                table: "Managers",
                column: "RepairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Repairs_RepairId",
                table: "Managers",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_Repairs_RepairId",
                table: "RepairLogs",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Repairs_RepairId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_Repairs_RepairId",
                table: "RepairLogs");

            migrationBuilder.DropIndex(
                name: "IX_Managers_RepairId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "RepairId",
                table: "Managers");

            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                table: "Repairs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "RepairId",
                table: "RepairLogs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "TechnicianId",
                table: "Managers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_ManagerId",
                table: "Repairs",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_TechnicianId",
                table: "Managers",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Technicians_TechnicianId",
                table: "Managers",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_Repairs_RepairId",
                table: "RepairLogs",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Managers_ManagerId",
                table: "Repairs",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
