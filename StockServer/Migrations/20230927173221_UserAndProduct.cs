using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockServer.Migrations
{
    /// <inheritdoc />
    public partial class UserAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Login",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Login_UserId",
                table: "Login",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Login_UserId",
                table: "Login",
                column: "UserId",
                principalTable: "Login",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Login_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Login_UserId",
                table: "Login");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Login_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Login_UserId",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Login");
        }
    }
}
