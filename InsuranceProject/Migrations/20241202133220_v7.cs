using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyAccounts");

            migrationBuilder.DropColumn(
                name: "PolicyStatus",
                table: "Policies");

            migrationBuilder.RenameColumn(
                name: "policyRatio",
                table: "Policies",
                newName: "policyProfitRatio");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Policies",
                newName: "PolicyName");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentType",
                table: "Policies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Benefits",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Benefits",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "policyProfitRatio",
                table: "Policies",
                newName: "policyRatio");

            migrationBuilder.RenameColumn(
                name: "PolicyName",
                table: "Policies",
                newName: "Title");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentType",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PolicyStatus",
                table: "Policies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PolicyAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IFSC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_PolicyAccounts_CustomerId",
                table: "PolicyAccounts",
                column: "CustomerId",
                unique: true);
        }
    }
}
