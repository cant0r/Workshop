using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class initialWorkshop20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobTechnician");

            migrationBuilder.CreateTable(
                name: "JobTechnicians",
                columns: table => new
                {
                    JobId = table.Column<long>(nullable: false),
                    TechnicianId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTechnicians", x => new { x.JobId, x.TechnicianId });
                    table.ForeignKey(
                        name: "FK_JobTechnicians_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobTechnicians_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTechnicians_TechnicianId",
                table: "JobTechnicians",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobTechnicians");

            migrationBuilder.CreateTable(
                name: "JobTechnician",
                columns: table => new
                {
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    TechnicianId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTechnician", x => new { x.JobId, x.TechnicianId });
                    table.ForeignKey(
                        name: "FK_JobTechnician_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobTechnician_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTechnician_TechnicianId",
                table: "JobTechnician",
                column: "TechnicianId");
        }
    }
}
