using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentPolicy");

            migrationBuilder.AddColumn<int>(
                name: "Payments",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PaymentId",
                table: "Policies",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Payments_PaymentId",
                table: "Policies",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Payments_PaymentId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_PaymentId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Payments",
                table: "Policies");

            migrationBuilder.CreateTable(
                name: "PaymentPolicy",
                columns: table => new
                {
                    PaymentsPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PoliciesPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPolicy", x => new { x.PaymentsPaymentId, x.PoliciesPolicyId });
                    table.ForeignKey(
                        name: "FK_PaymentPolicy_Payments_PaymentsPaymentId",
                        column: x => x.PaymentsPaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentPolicy_Policies_PoliciesPolicyId",
                        column: x => x.PoliciesPolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPolicy_PoliciesPolicyId",
                table: "PaymentPolicy",
                column: "PoliciesPolicyId");
        }
    }
}
