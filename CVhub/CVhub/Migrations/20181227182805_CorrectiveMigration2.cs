using Microsoft.EntityFrameworkCore.Migrations;

namespace CVhub.Migrations
{
    public partial class CorrectiveMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyName",
                table: "JobOffers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "JobOffers");
        }
    }
}
