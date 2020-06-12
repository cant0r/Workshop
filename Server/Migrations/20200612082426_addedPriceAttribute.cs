using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class addedPriceAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Repairs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Percentage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Repairs");
        }
    }
}
