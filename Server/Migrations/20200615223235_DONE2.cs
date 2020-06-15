using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class DONE2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Repairs_RepairId",
                table: "Bonuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bonuses",
                table: "Bonuses");

            migrationBuilder.DropIndex(
                name: "IX_Bonuses_RepairId",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "RepairId",
                table: "Bonuses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bonuses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Bonuses",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bonuses",
                table: "Bonuses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BonusRepairs",
                columns: table => new
                {
                    RepairID = table.Column<long>(nullable: false),
                    BonusName = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusRepairs", x => new { x.RepairID, x.BonusName });
                    table.ForeignKey(
                        name: "FK_BonusRepairs_Bonuses_BonusName",
                        column: x => x.BonusName,
                        principalTable: "Bonuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonusRepairs_Repairs_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusRepairs_BonusName",
                table: "BonusRepairs",
                column: "BonusName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusRepairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bonuses",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bonuses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bonuses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "RepairId",
                table: "Bonuses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bonuses",
                table: "Bonuses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_RepairId",
                table: "Bonuses",
                column: "RepairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Repairs_RepairId",
                table: "Bonuses",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
