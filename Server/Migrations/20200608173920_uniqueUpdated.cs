using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class uniqueUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Repairs_RepairId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_RepairId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "RepairId",
                table: "Managers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Technicians",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Managers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TechnicianId",
                table: "Managers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_Email",
                table: "Technicians",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_Email",
                table: "Managers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_TechnicianId",
                table: "Managers",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Technicians_TechnicianId",
                table: "Managers",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Technicians_TechnicianId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_Email",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Managers_Email",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_TechnicianId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "Managers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Technicians",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RepairId",
                table: "Managers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
        }
    }
}
