using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class DONE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    isManager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Automobiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    LicencePlate = table.Column<string>(nullable: false),
                    ClientId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automobiles_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technicians_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoId = table.Column<long>(nullable: false),
                    ManagerId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<long>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Automobiles_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Automobiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repairs_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    RepairId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Bonuses_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicianId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    RepairId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairLogs_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairTechnicians",
                columns: table => new
                {
                    RepairID = table.Column<long>(nullable: false),
                    TechnicianId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairTechnicians", x => new { x.RepairID, x.TechnicianId });
                    table.ForeignKey(
                        name: "FK_RepairTechnicians_Repairs_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repairs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairTechnicians_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_ClientId",
                table: "Automobiles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_LicencePlate",
                table: "Automobiles",
                column: "LicencePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_RepairId",
                table: "Bonuses",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairLogs_RepairId",
                table: "RepairLogs",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_AutoId",
                table: "Repairs",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_ManagerId",
                table: "Repairs",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTechnicians_TechnicianId",
                table: "RepairTechnicians",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_UserId",
                table: "Technicians",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "RepairLogs");

            migrationBuilder.DropTable(
                name: "RepairTechnicians");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Technicians");

            migrationBuilder.DropTable(
                name: "Automobiles");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
