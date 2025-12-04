using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinaControl.Migrations
{
    /// <inheritdoc />
    public partial class CriadoCampoPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Payment",
                table: "Transaction",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Transaction");
        }
    }
}
