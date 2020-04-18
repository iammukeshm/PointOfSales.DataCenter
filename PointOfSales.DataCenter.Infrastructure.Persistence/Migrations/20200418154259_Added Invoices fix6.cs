using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Migrations
{
    public partial class AddedInvoicesfix6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "InvoiceTransactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InvoiceTransactions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "InvoiceTransactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "InvoiceTransactions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "InvoiceDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InvoiceDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "InvoiceDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "InvoiceDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "InvoiceTransactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InvoiceTransactions");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "InvoiceTransactions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "InvoiceTransactions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "InvoiceDetails");
        }
    }
}
