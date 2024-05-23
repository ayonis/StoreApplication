using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class Update_DeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryTransactions_Orders_OrderId",
                table: "TreasuryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_TreasuryTransactions_OrderId",
                table: "TreasuryTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "TreasuryTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TreasuryTransactions_OrderId",
                table: "TreasuryTransactions",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryTransactions_Orders_OrderId",
                table: "TreasuryTransactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "TreasuryTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
