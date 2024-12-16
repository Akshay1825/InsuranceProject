using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Commissions");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Commissions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Commissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "policyNumber",
                table: "Commissions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "policyNumber",
                table: "Commissions");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Commissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
