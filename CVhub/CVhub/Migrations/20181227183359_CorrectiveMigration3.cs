using Microsoft.EntityFrameworkCore.Migrations;

namespace CVhub.Migrations
{
    public partial class CorrectiveMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "JobOffers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CompanyName",
                table: "JobOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
