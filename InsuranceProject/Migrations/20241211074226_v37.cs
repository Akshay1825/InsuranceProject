using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_Policies_PolicyNo",
                table: "Commissions");

            migrationBuilder.RenameColumn(
                name: "PolicyNo",
                table: "Commissions",
                newName: "PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_Commissions_PolicyNo",
                table: "Commissions",
                newName: "IX_Commissions_PolicyId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Commissions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "CommissionType",
                table: "Commissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_Policies_PolicyId",
                table: "Commissions",
                column: "PolicyId",
                principalTable: "Policies",
                principalColumn: "PolicyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_Policies_PolicyId",
                table: "Commissions");

            migrationBuilder.RenameColumn(
                name: "PolicyId",
                table: "Commissions",
                newName: "PolicyNo");

            migrationBuilder.RenameIndex(
                name: "IX_Commissions_PolicyId",
                table: "Commissions",
                newName: "IX_Commissions_PolicyNo");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Commissions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CommissionType",
                table: "Commissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_Policies_PolicyNo",
                table: "Commissions",
                column: "PolicyNo",
                principalTable: "Policies",
                principalColumn: "PolicyId");
        }
    }
}
