using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Migrations
{
    public partial class AddedInvoicesfix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTaxable",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTaxable",
                table: "Products");
        }
    }
}
