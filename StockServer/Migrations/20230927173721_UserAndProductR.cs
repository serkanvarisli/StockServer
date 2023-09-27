using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockServer.Migrations
{
    /// <inheritdoc />
    public partial class UserAndProductR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Login_UserId",
                table: "Login");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Login_UserId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Login",
                table: "Login");

            migrationBuilder.RenameTable(
                name: "Login",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_Login_UserId",
                table: "Users",
                newName: "IX_Users_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Login");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserId",
                table: "Login",
                newName: "IX_Login_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Login",
                table: "Login",
                column: "Id");

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
    }
}
