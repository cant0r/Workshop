using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class BonusUpdate14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Technicians_TechnicianId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_TechnicianId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "Managers");

            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                table: "Repairs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_ManagerId",
                table: "Repairs",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Managers_ManagerId",
                table: "Repairs",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Managers_ManagerId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_ManagerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Repairs");

            migrationBuilder.AddColumn<long>(
                name: "TechnicianId",
                table: "Managers",
                type: "bigint",
                nullable: true);

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
        }
    }
}
