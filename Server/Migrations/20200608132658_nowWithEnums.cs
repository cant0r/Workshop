using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class nowWithEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_Repairs_JobId",
                table: "RepairLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Managers_JobManagerId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_States_WorkStateId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairTechnicians_Repairs_JobId",
                table: "RepairTechnicians");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairTechnicians",
                table: "RepairTechnicians");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_JobManagerId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_WorkStateId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_RepairLogs_JobId",
                table: "RepairLogs");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "RepairTechnicians");

            migrationBuilder.DropColumn(
                name: "JobManagerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "WorkStateId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "RepairLogs");

            migrationBuilder.AddColumn<long>(
                name: "RepairID",
                table: "RepairTechnicians",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RepairManagerId",
                table: "Repairs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WorkState",
                table: "Repairs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "RepairId",
                table: "RepairLogs",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairTechnicians",
                table: "RepairTechnicians",
                columns: new[] { "RepairID", "TechnicianId" });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairManagerId",
                table: "Repairs",
                column: "RepairManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairLogs_RepairId",
                table: "RepairLogs",
                column: "RepairId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_Repairs_RepairId",
                table: "RepairLogs",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Managers_RepairManagerId",
                table: "Repairs",
                column: "RepairManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairTechnicians_Repairs_RepairID",
                table: "RepairTechnicians",
                column: "RepairID",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_Repairs_RepairId",
                table: "RepairLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Managers_RepairManagerId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairTechnicians_Repairs_RepairID",
                table: "RepairTechnicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairTechnicians",
                table: "RepairTechnicians");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_RepairManagerId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_RepairLogs_RepairId",
                table: "RepairLogs");

            migrationBuilder.DropColumn(
                name: "RepairID",
                table: "RepairTechnicians");

            migrationBuilder.DropColumn(
                name: "RepairManagerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "WorkState",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairId",
                table: "RepairLogs");

            migrationBuilder.AddColumn<long>(
                name: "JobId",
                table: "RepairTechnicians",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "JobManagerId",
                table: "Repairs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WorkStateId",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "JobId",
                table: "RepairLogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairTechnicians",
                table: "RepairTechnicians",
                columns: new[] { "JobId", "TechnicianId" });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_JobManagerId",
                table: "Repairs",
                column: "JobManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_WorkStateId",
                table: "Repairs",
                column: "WorkStateId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairLogs_JobId",
                table: "RepairLogs",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_Repairs_JobId",
                table: "RepairLogs",
                column: "JobId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Managers_JobManagerId",
                table: "Repairs",
                column: "JobManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_States_WorkStateId",
                table: "Repairs",
                column: "WorkStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairTechnicians_Repairs_JobId",
                table: "RepairTechnicians",
                column: "JobId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
