using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Employees_EmployeeId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceSchemes_SchemeDetails_DetailId",
                table: "InsuranceSchemes");

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
                name: "CustomerPolicy");

            migrationBuilder.DropTable(
                name: "InsurancePlanInsuranceScheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policies",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_ClaimmClaimId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceSchemes_DetailId",
                table: "InsuranceSchemes");

            migrationBuilder.DropIndex(
                name: "IX_Agents_EmployeeId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Benefits",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Description",
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
                name: "MinPolicyTerm",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyName",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "policyProfitRatio",
                table: "Policies",
                newName: "PremiumType");

            migrationBuilder.RenameColumn(
                name: "RegistrationCommisionAmount",
                table: "Policies",
                newName: "SumAssured");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Policies",
                newName: "TaxId");

            migrationBuilder.RenameColumn(
                name: "MinAmount",
                table: "Policies",
                newName: "PremiumAmount");

            migrationBuilder.RenameColumn(
                name: "InsuranceSchemeSchemeId",
                table: "Policies",
                newName: "PolicyTypeId");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "Policies",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "ClaimmClaimId",
                table: "Policies",
                newName: "InsuranceSettingId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_PaymentId",
                table: "Policies",
                newName: "IX_Policies_TaxId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_InsuranceSchemeSchemeId",
                table: "Policies",
                newName: "IX_Policies_PolicyTypeId");

            migrationBuilder.RenameColumn(
                name: "DetailId",
                table: "InsuranceSchemes",
                newName: "PlanId");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Documents",
                newName: "FilePath");

            migrationBuilder.AddColumn<Guid>(
                name: "InsuranceSchemeSchemeId",
                table: "SchemeDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PolicyId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                table: "Policies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClaimId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InstallmentAmount",
                table: "Policies",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsuranceSchemeId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsuranceSettingsId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MaturityDate",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "PolicyTerm",
                table: "Policies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "TotalPaidAmount",
                table: "Policies",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlanName",
                table: "Plans",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CVVNo",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SchemeName",
                table: "InsuranceSchemes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InsuranceSchemes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "InstallmentCommRatio",
                table: "InsuranceSchemes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaxAge",
                table: "InsuranceSchemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MaxAmount",
                table: "InsuranceSchemes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaxInvestTime",
                table: "InsuranceSchemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinAge",
                table: "InsuranceSchemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MinAmount",
                table: "InsuranceSchemes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MinInvestTime",
                table: "InsuranceSchemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ProfitRatio",
                table: "InsuranceSchemes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RegistrationCommRatio",
                table: "InsuranceSchemes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RequiredDocuments",
                table: "InsuranceSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "SchemeImage",
                table: "InsuranceSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "Salary",
                table: "Employees",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOJ",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PolicyId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policies",
                table: "Policies",
                column: "PolicyId");

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    CommissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommissionType = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyNo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.CommissionId);
                    table.ForeignKey(
                        name: "FK_Commissions_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commissions_Policies_PolicyNo",
                        column: x => x.PolicyNo,
                        principalTable: "Policies",
                        principalColumn: "PolicyId");
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    InstallmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountDue = table.Column<double>(type: "float", nullable: false),
                    AmountPaid = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.InstallmentId);
                    table.ForeignKey(
                        name: "FK_Installments_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimDeductionPercentage = table.Column<double>(type: "float", nullable: true),
                    PenaltyDeductionPercentage = table.Column<double>(type: "float", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nominees",
                columns: table => new
                {
                    NomineeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomineeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomineeRelation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominees", x => x.NomineeId);
                    table.ForeignKey(
                        name: "FK_Nominees_Policies_PolicyNumber",
                        column: x => x.PolicyNumber,
                        principalTable: "Policies",
                        principalColumn: "PolicyId");
                });

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

            migrationBuilder.CreateTable(
                name: "PolicyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxs",
                columns: table => new
                {
                    TaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxPercentage = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxs", x => x.TaxId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchemeDetails_InsuranceSchemeSchemeId",
                table: "SchemeDetails",
                column: "InsuranceSchemeSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_AgentId",
                table: "Policies",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ClaimId",
                table: "Policies",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CustomerId",
                table: "Policies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_InsuranceSchemeId",
                table: "Policies",
                column: "InsuranceSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_InsuranceSettingsId",
                table: "Policies",
                column: "InsuranceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceSchemes_PlanId",
                table: "InsuranceSchemes",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PolicyId",
                table: "Documents",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_AgentId",
                table: "Commissions",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_PolicyNo",
                table: "Commissions",
                column: "PolicyNo");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_PolicyId",
                table: "Installments",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominees_PolicyNumber",
                table: "Nominees",
                column: "PolicyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPolicy_PoliciesPolicyId",
                table: "PaymentPolicy",
                column: "PoliciesPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Policies_PolicyId",
                table: "Documents",
                column: "PolicyId",
                principalTable: "Policies",
                principalColumn: "PolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceSchemes_Plans_PlanId",
                table: "InsuranceSchemes",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Agents_AgentId",
                table: "Policies",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceSchemes_InsuranceSchemeId",
                table: "Policies",
                column: "InsuranceSchemeId",
                principalTable: "InsuranceSchemes",
                principalColumn: "SchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceSettings_InsuranceSettingsId",
                table: "Policies",
                column: "InsuranceSettingsId",
                principalTable: "InsuranceSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_PolicyTypes_PolicyTypeId",
                table: "Policies",
                column: "PolicyTypeId",
                principalTable: "PolicyTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Taxs_TaxId",
                table: "Policies",
                column: "TaxId",
                principalTable: "Taxs",
                principalColumn: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_claims_ClaimId",
                table: "Policies",
                column: "ClaimId",
                principalTable: "claims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchemeDetails_InsuranceSchemes_InsuranceSchemeSchemeId",
                table: "SchemeDetails",
                column: "InsuranceSchemeSchemeId",
                principalTable: "InsuranceSchemes",
                principalColumn: "SchemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Policies_PolicyId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceSchemes_Plans_PlanId",
                table: "InsuranceSchemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Agents_AgentId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceSchemes_InsuranceSchemeId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceSettings_InsuranceSettingsId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_PolicyTypes_PolicyTypeId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Taxs_TaxId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_claims_ClaimId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_SchemeDetails_InsuranceSchemes_InsuranceSchemeSchemeId",
                table: "SchemeDetails");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "InsuranceSettings");

            migrationBuilder.DropTable(
                name: "Nominees");

            migrationBuilder.DropTable(
                name: "PaymentPolicy");

            migrationBuilder.DropTable(
                name: "PolicyTypes");

            migrationBuilder.DropTable(
                name: "Taxs");

            migrationBuilder.DropIndex(
                name: "IX_SchemeDetails_InsuranceSchemeSchemeId",
                table: "SchemeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policies",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_AgentId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_ClaimId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_CustomerId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_InsuranceSchemeId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_InsuranceSettingsId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceSchemes_PlanId",
                table: "InsuranceSchemes");

            migrationBuilder.DropIndex(
                name: "IX_Documents_PolicyId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "InsuranceSchemeSchemeId",
                table: "SchemeDetails");

            migrationBuilder.DropColumn(
                name: "PolicyId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "InstallmentAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "InsuranceSchemeId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "InsuranceSettingsId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MaturityDate",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyTerm",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "TotalPaidAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CVVNo",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "InstallmentCommRatio",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "MaxAge",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "MaxAmount",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "MaxInvestTime",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "MinAmount",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "MinInvestTime",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "ProfitRatio",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "RegistrationCommRatio",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "RequiredDocuments",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "SchemeImage",
                table: "InsuranceSchemes");

            migrationBuilder.DropColumn(
                name: "DOJ",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DocType",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "PolicyId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "TaxId",
                table: "Policies",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "SumAssured",
                table: "Policies",
                newName: "RegistrationCommisionAmount");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Policies",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "PremiumType",
                table: "Policies",
                newName: "policyProfitRatio");

            migrationBuilder.RenameColumn(
                name: "PremiumAmount",
                table: "Policies",
                newName: "MinAmount");

            migrationBuilder.RenameColumn(
                name: "PolicyTypeId",
                table: "Policies",
                newName: "InsuranceSchemeSchemeId");

            migrationBuilder.RenameColumn(
                name: "InsuranceSettingId",
                table: "Policies",
                newName: "ClaimmClaimId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_TaxId",
                table: "Policies",
                newName: "IX_Policies_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_PolicyTypeId",
                table: "Policies",
                newName: "IX_Policies_InsuranceSchemeSchemeId");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "InsuranceSchemes",
                newName: "DetailId");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Documents",
                newName: "Location");

            migrationBuilder.AddColumn<string>(
                name: "Benefits",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Policies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "MinPolicyTerm",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PolicyName",
                table: "Policies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PlanName",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "SchemeName",
                table: "InsuranceSchemes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<double>(
                name: "Salary",
                table: "Employees",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policies",
                table: "Policies",
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
                name: "IX_InsuranceSchemes_DetailId",
                table: "InsuranceSchemes",
                column: "DetailId",
                unique: true,
                filter: "[DetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_EmployeeId",
                table: "Agents",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPolicy_PoliciesId",
                table: "CustomerPolicy",
                column: "PoliciesId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePlanInsuranceScheme_SchemesSchemeId",
                table: "InsurancePlanInsuranceScheme",
                column: "SchemesSchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Employees_EmployeeId",
                table: "Agents",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceSchemes_SchemeDetails_DetailId",
                table: "InsuranceSchemes",
                column: "DetailId",
                principalTable: "SchemeDetails",
                principalColumn: "DetailId");

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
    }
}
