using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceSettings_InsuranceSettingsId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_claims_ClaimId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_ClaimId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "SchemeImage",
                table: "InsuranceSchemes");

            migrationBuilder.AlterColumn<Guid>(
                name: "InsuranceSettingsId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ClaimmClaimId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ClaimmClaimId",
                table: "Policies",
                column: "ClaimmClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceSettings_InsuranceSettingsId",
                table: "Policies",
                column: "InsuranceSettingsId",
                principalTable: "InsuranceSettings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_claims_ClaimmClaimId",
                table: "Policies",
                column: "ClaimmClaimId",
                principalTable: "claims",
                principalColumn: "ClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceSettings_InsuranceSettingsId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_claims_ClaimmClaimId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_ClaimmClaimId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "ClaimmClaimId",
                table: "Policies");

            migrationBuilder.AlterColumn<Guid>(
                name: "InsuranceSettingsId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClaimId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SchemeImage",
                table: "InsuranceSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ClaimId",
                table: "Policies",
                column: "ClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceSettings_InsuranceSettingsId",
                table: "Policies",
                column: "InsuranceSettingsId",
                principalTable: "InsuranceSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_claims_ClaimId",
                table: "Policies",
                column: "ClaimId",
                principalTable: "claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
