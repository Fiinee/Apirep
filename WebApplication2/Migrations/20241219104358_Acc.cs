using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class Acc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Account",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "RefreshToken",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "RefreshToken",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "id");
        }
    }
}
