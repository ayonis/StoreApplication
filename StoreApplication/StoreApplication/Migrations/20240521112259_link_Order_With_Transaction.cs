using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class link_Order_With_Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Type",
                table: "TreasuryTransactions",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "TreasuryAccounts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<double>(
                name: "FinalCost",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TreasuryTransactionId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TreasuryTransactionId",
                table: "Orders",
                column: "TreasuryTransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TreasuryTransactions_TreasuryTransactionId",
                table: "Orders",
                column: "TreasuryTransactionId",
                principalTable: "TreasuryTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TreasuryTransactions_TreasuryTransactionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TreasuryTransactionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FinalCost",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TreasuryTransactionId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "TreasuryTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "TreasuryAccounts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
