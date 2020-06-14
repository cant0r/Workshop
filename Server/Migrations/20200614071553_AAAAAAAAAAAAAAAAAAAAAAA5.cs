using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class AAAAAAAAAAAAAAAAAAAAAAA5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RepairId1",
                table: "Bonuses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_RepairId1",
                table: "Bonuses",
                column: "RepairId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Repairs_RepairId1",
                table: "Bonuses",
                column: "RepairId1",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Repairs_RepairId1",
                table: "Bonuses");

            migrationBuilder.DropIndex(
                name: "IX_Bonuses_RepairId1",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "RepairId1",
                table: "Bonuses");
        }
    }
}
