using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SchemeDetails",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Policies",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Plans",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Payments",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Nominees",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InsuranceSchemes",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Installments",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Complaints",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Commissions",
                newName: "BaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "claims",
                newName: "BaseId");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "States",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "PolicyTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "InsuranceSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "PolicyTypes");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "InsuranceSettings");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "SchemeDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Policies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Plans",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Payments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Nominees",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "InsuranceSchemes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Installments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Complaints",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "Commissions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaseId",
                table: "claims",
                newName: "Id");
        }
    }
}
