using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class Order_Transaction_Relation_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TreasuryTransactions_TreasuryTransactionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TreasuryTransactionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TreasuryTransactionId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderNum",
                table: "TreasuryTransactions",
                newName: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TreasuryTransactions_OrderId",
                table: "TreasuryTransactions",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryTransactions_Orders_OrderId",
                table: "TreasuryTransactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryTransactions_Orders_OrderId",
                table: "TreasuryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_TreasuryTransactions_OrderId",
                table: "TreasuryTransactions");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "TreasuryTransactions",
                newName: "OrderNum");

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
    }
}
