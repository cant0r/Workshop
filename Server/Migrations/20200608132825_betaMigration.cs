using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class betaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Managers_RepairManagerId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_RepairManagerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairManagerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "WorkState",
                table: "Repairs");

            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                table: "Repairs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Repairs",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropColumn(
                name: "State",
                table: "Repairs");

            migrationBuilder.AddColumn<long>(
                name: "RepairManagerId",
                table: "Repairs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WorkState",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairManagerId",
                table: "Repairs",
                column: "RepairManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Managers_RepairManagerId",
                table: "Repairs",
                column: "RepairManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
