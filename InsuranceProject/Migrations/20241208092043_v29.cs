using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Taxs_TaxId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_TaxId",
                table: "Policies");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseId",
                table: "Taxs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Taxs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TaxSettingsTaxId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_TaxSettingsTaxId",
                table: "Policies",
                column: "TaxSettingsTaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Taxs_TaxSettingsTaxId",
                table: "Policies",
                column: "TaxSettingsTaxId",
                principalTable: "Taxs",
                principalColumn: "TaxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Taxs_TaxSettingsTaxId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_TaxSettingsTaxId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Taxs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Taxs");

            migrationBuilder.DropColumn(
                name: "TaxSettingsTaxId",
                table: "Policies");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_TaxId",
                table: "Policies",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Taxs_TaxId",
                table: "Policies",
                column: "TaxId",
                principalTable: "Taxs",
                principalColumn: "TaxId");
        }
    }
}
