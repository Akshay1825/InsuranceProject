using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_RoleNameRoleId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Admins_AdminId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AdminId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleNameRoleId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Policies_CustomerId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyDescription",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyName",
                table: "Policies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleNameRoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PolicyId",
                table: "Policies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Documents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "Agents",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoleName",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Policies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentCommisionRatio",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxAge",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MaxAmount",
                table: "Policies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaxPolicyTerm",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinAge",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MinAmount",
                table: "Policies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MinPolicyTerm",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PolicyStatus",
                table: "Policies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "RegistrationCommisionAmount",
                table: "Policies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Policies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "policyRatio",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalCommissionEarned",
                table: "Agents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalWithdrawalAmount",
                table: "Agents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomerPolicy",
                columns: table => new
                {
                    CustomersCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PoliciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPolicy", x => new { x.CustomersCustomerId, x.PoliciesId });
                    table.ForeignKey(
                        name: "FK_CustomerPolicy_Customers_CustomersCustomerId",
                        column: x => x.CustomersCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPolicy_Policies_PoliciesId",
                        column: x => x.PoliciesId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IFSC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyAccounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AdminId",
                table: "Employees",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPolicy_PoliciesId",
                table: "CustomerPolicy",
                column: "PoliciesId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyAccounts_CustomerId",
                table: "PolicyAccounts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Admins_AdminId",
                table: "Employees",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Admins_AdminId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CustomerPolicy");

            migrationBuilder.DropTable(
                name: "PolicyAccounts");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AdminId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "InstallmentCommisionRatio",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MaxAge",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MaxAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MaxPolicyTerm",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MinAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MinPolicyTerm",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyStatus",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "RegistrationCommisionAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "policyRatio",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TotalCommissionEarned",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "TotalWithdrawalAmount",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleNameRoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Policies",
                newName: "PolicyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Documents",
                newName: "DocumentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Agents",
                newName: "AgentId");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolicyDescription",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PolicyName",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdminId",
                table: "Users",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleNameRoleId",
                table: "Roles",
                column: "RoleNameRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CustomerId",
                table: "Policies",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Roles_RoleNameRoleId",
                table: "Roles",
                column: "RoleNameRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Admins_AdminId",
                table: "Users",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
