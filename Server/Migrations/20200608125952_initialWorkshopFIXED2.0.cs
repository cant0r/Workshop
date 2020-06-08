using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class initialWorkshopFIXED20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobLogs_Jobs_JobId",
                table: "JobLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Automobiles_AutoId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Managers_JobManagerId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_States_WorkStateId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTechnicians_Jobs_JobId",
                table: "JobTechnicians");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTechnicians_Technicians_TechnicianId",
                table: "JobTechnicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTechnicians",
                table: "JobTechnicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobLogs",
                table: "JobLogs");

            migrationBuilder.RenameTable(
                name: "JobTechnicians",
                newName: "RepairTechnicians");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Repairs");

            migrationBuilder.RenameTable(
                name: "JobLogs",
                newName: "RepairLogs");

            migrationBuilder.RenameIndex(
                name: "IX_JobTechnicians_TechnicianId",
                table: "RepairTechnicians",
                newName: "IX_RepairTechnicians_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_WorkStateId",
                table: "Repairs",
                newName: "IX_Repairs_WorkStateId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_JobManagerId",
                table: "Repairs",
                newName: "IX_Repairs_JobManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_AutoId",
                table: "Repairs",
                newName: "IX_Repairs_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_JobLogs_JobId",
                table: "RepairLogs",
                newName: "IX_RepairLogs_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairTechnicians",
                table: "RepairTechnicians",
                columns: new[] { "JobId", "TechnicianId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairLogs",
                table: "RepairLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_Repairs_JobId",
                table: "RepairLogs",
                column: "JobId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Automobiles_AutoId",
                table: "Repairs",
                column: "AutoId",
                principalTable: "Automobiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_RepairTechnicians_Technicians_TechnicianId",
                table: "RepairTechnicians",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_Repairs_JobId",
                table: "RepairLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Automobiles_AutoId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Managers_JobManagerId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_States_WorkStateId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairTechnicians_Repairs_JobId",
                table: "RepairTechnicians");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairTechnicians_Technicians_TechnicianId",
                table: "RepairTechnicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairTechnicians",
                table: "RepairTechnicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairLogs",
                table: "RepairLogs");

            migrationBuilder.RenameTable(
                name: "RepairTechnicians",
                newName: "JobTechnicians");

            migrationBuilder.RenameTable(
                name: "Repairs",
                newName: "Jobs");

            migrationBuilder.RenameTable(
                name: "RepairLogs",
                newName: "JobLogs");

            migrationBuilder.RenameIndex(
                name: "IX_RepairTechnicians_TechnicianId",
                table: "JobTechnicians",
                newName: "IX_JobTechnicians_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_WorkStateId",
                table: "Jobs",
                newName: "IX_Jobs_WorkStateId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_JobManagerId",
                table: "Jobs",
                newName: "IX_Jobs_JobManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_AutoId",
                table: "Jobs",
                newName: "IX_Jobs_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairLogs_JobId",
                table: "JobLogs",
                newName: "IX_JobLogs_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTechnicians",
                table: "JobTechnicians",
                columns: new[] { "JobId", "TechnicianId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobLogs",
                table: "JobLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobLogs_Jobs_JobId",
                table: "JobLogs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Automobiles_AutoId",
                table: "Jobs",
                column: "AutoId",
                principalTable: "Automobiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Managers_JobManagerId",
                table: "Jobs",
                column: "JobManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_States_WorkStateId",
                table: "Jobs",
                column: "WorkStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTechnicians_Jobs_JobId",
                table: "JobTechnicians",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTechnicians_Technicians_TechnicianId",
                table: "JobTechnicians",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
