using Microsoft.EntityFrameworkCore.Migrations;

namespace UL.Calculator.Data.Migrations
{
    public partial class Seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "s.Thompson@ul.com", "Simon ", "Thompson " });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 2, "s.Gibbens@ul.com", "Stephanie", "Gibbens" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 3, "free@free.com", "Free", "User" });

            migrationBuilder.InsertData(
                table: "UserLogin",
                columns: new[] { "Username", "Password", "SubscriptionType" },
                values: new object[] { "s.Thompson@ul.com", "admin", "Premium" });

            migrationBuilder.InsertData(
                table: "UserLogin",
                columns: new[] { "Username", "Password", "SubscriptionType" },
                values: new object[] { "s.Gibbens@ul.com", "admin", "Standard" });

            migrationBuilder.InsertData(
                table: "UserLogin",
                columns: new[] { "Username", "Password", "SubscriptionType" },
                values: new object[] { "free@free.com", "user", "Free" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserLogin",
                keyColumn: "Username",
                keyValue: "free@free.com");

            migrationBuilder.DeleteData(
                table: "UserLogin",
                keyColumn: "Username",
                keyValue: "s.Gibbens@ul.com");

            migrationBuilder.DeleteData(
                table: "UserLogin",
                keyColumn: "Username",
                keyValue: "s.Thompson@ul.com");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
