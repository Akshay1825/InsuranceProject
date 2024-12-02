using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Admins_AdminId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AdminId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdminId",
                table: "Users",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Admins_AdminId",
                table: "Users",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Admins_AdminId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AdminId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AdminId",
                table: "Employees",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Admins_AdminId",
                table: "Employees",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }
    }
}
