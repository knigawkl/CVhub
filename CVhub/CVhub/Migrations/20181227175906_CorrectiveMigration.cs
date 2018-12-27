using Microsoft.EntityFrameworkCore.Migrations;

namespace CVhub.Migrations
{
    public partial class CorrectiveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobOfers_JobOfferId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfers_Companies_CompanyId",
                table: "JobOfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOfers",
                table: "JobOfers");

            migrationBuilder.RenameTable(
                name: "JobOfers",
                newName: "JobOffers");

            migrationBuilder.RenameIndex(
                name: "IX_JobOfers_CompanyId",
                table: "JobOffers",
                newName: "IX_JobOffers_CompanyId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "JobApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOffers",
                table: "JobOffers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferId",
                table: "JobApplications",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobOffers",
                table: "JobOffers");

            migrationBuilder.RenameTable(
                name: "JobOffers",
                newName: "JobOfers");

            migrationBuilder.RenameIndex(
                name: "IX_JobOffers_CompanyId",
                table: "JobOfers",
                newName: "IX_JobOfers_CompanyId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobOfers",
                table: "JobOfers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobOfers_JobOfferId",
                table: "JobApplications",
                column: "JobOfferId",
                principalTable: "JobOfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfers_Companies_CompanyId",
                table: "JobOfers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
