using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClaimmClaimId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsuranceSchemeSchemeId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "claims",
                columns: table => new
                {
                    ClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimAmount = table.Column<double>(type: "float", nullable: true),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankIFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claims", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplaintName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfComplaint = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ComplaintId);
                    table.ForeignKey(
                        name: "FK_Complaints_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    TotalPayment = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "SchemeDetails",
                columns: table => new
                {
                    DetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemeImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinAmount = table.Column<int>(type: "int", nullable: false),
                    MaxAmount = table.Column<int>(type: "int", nullable: false),
                    MinAge = table.Column<int>(type: "int", nullable: false),
                    MaxAge = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeDetails", x => x.DetailId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceSchemes",
                columns: table => new
                {
                    SchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    DetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceSchemes", x => x.SchemeId);
                    table.ForeignKey(
                        name: "FK_InsuranceSchemes_SchemeDetails_DetailId",
                        column: x => x.DetailId,
                        principalTable: "SchemeDetails",
                        principalColumn: "DetailId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePlanInsuranceScheme",
                columns: table => new
                {
                    PlansPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemesSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePlanInsuranceScheme", x => new { x.PlansPlanId, x.SchemesSchemeId });
                    table.ForeignKey(
                        name: "FK_InsurancePlanInsuranceScheme_InsuranceSchemes_SchemesSchemeId",
                        column: x => x.SchemesSchemeId,
                        principalTable: "InsuranceSchemes",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurancePlanInsuranceScheme_Plans_PlansPlanId",
                        column: x => x.PlansPlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ClaimmClaimId",
                table: "Policies",
                column: "ClaimmClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_InsuranceSchemeSchemeId",
                table: "Policies",
                column: "InsuranceSchemeSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PaymentId",
                table: "Policies",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_CustomerId",
                table: "Complaints",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePlanInsuranceScheme_SchemesSchemeId",
                table: "InsurancePlanInsuranceScheme",
                column: "SchemesSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceSchemes_DetailId",
                table: "InsuranceSchemes",
                column: "DetailId",
                unique: true,
                filter: "[DetailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceSchemes_InsuranceSchemeSchemeId",
                table: "Policies",
                column: "InsuranceSchemeSchemeId",
                principalTable: "InsuranceSchemes",
                principalColumn: "SchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Payments_PaymentId",
                table: "Policies",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId");

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
                name: "FK_Policies_InsuranceSchemes_InsuranceSchemeSchemeId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Payments_PaymentId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_claims_ClaimmClaimId",
                table: "Policies");

            migrationBuilder.DropTable(
                name: "claims");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "InsurancePlanInsuranceScheme");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "InsuranceSchemes");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "SchemeDetails");

            migrationBuilder.DropIndex(
                name: "IX_Policies_ClaimmClaimId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_InsuranceSchemeSchemeId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_PaymentId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "ClaimmClaimId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "InsuranceSchemeSchemeId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Policies");
        }
    }
}
