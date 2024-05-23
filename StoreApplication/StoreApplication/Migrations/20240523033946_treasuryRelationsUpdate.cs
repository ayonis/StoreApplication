using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class treasuryRelationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryTransactions_TreasuryAccounts_TreasuryAccountId",
                table: "TreasuryTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "TreasuryAccountId",
                table: "TreasuryTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryTransactions_TreasuryAccounts_TreasuryAccountId",
                table: "TreasuryTransactions",
                column: "TreasuryAccountId",
                principalTable: "TreasuryAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryTransactions_TreasuryAccounts_TreasuryAccountId",
                table: "TreasuryTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "TreasuryAccountId",
                table: "TreasuryTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryTransactions_TreasuryAccounts_TreasuryAccountId",
                table: "TreasuryTransactions",
                column: "TreasuryAccountId",
                principalTable: "TreasuryAccounts",
                principalColumn: "Id");
        }
    }
}
