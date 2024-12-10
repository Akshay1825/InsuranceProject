using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvestmentAmount",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPremiumNumber",
                table: "Policies",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvestmentAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "TotalPremiumNumber",
                table: "Policies");
        }
    }
}
