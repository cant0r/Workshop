using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class addedMoreThingsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Discounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Technicians",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Technicians",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "TechnicianId",
                table: "RepairLogs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Discounts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RepairId",
                table: "Discounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_RepairId",
                table: "Discounts",
                column: "RepairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Repairs_RepairId",
                table: "Discounts",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Repairs_RepairId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_RepairId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "RepairLogs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "RepairId",
                table: "Discounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Technicians",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
