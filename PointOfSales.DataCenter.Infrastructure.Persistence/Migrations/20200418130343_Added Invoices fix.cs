using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Migrations
{
    public partial class AddedInvoicesfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_People_CustomerId1",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CustomerId1",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_People_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_People_CustomerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId1",
                table: "Invoices",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_People_CustomerId1",
                table: "Invoices",
                column: "CustomerId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
