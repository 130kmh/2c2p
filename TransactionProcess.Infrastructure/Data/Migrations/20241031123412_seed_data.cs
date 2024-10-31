using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransactionProcess.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3678));

            migrationBuilder.UpdateData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3688));

            migrationBuilder.InsertData(
                table: "TransactionRecords",
                columns: new[] { "Id", "AccountNumber", "Amount", "CreateDate", "CreateUser", "CurrencyCode", "Status", "TransactionDate", "TransactionId" },
                values: new object[,]
                {
                    { 3, "2345", 500.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3695), "testuser", "GBP", "D", new DateTime(2024, 3, 1, 14, 23, 10, 0, DateTimeKind.Unspecified), "Invoice0000003" },
                    { 4, "6789", 1200.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3701), "testuser", "USD", "A", new DateTime(2024, 3, 10, 10, 30, 5, 0, DateTimeKind.Unspecified), "Invoice0000004" },
                    { 5, "3456", 750.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3708), "testuser", "CAD", "R", new DateTime(2024, 4, 15, 18, 45, 25, 0, DateTimeKind.Unspecified), "Invoice0000005" },
                    { 6, "7890", 200.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3714), "testuser", "AUD", "D", new DateTime(2024, 5, 20, 8, 15, 30, 0, DateTimeKind.Unspecified), "Invoice0000006" },
                    { 7, "4567", 950.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3720), "testuser", "EUR", "A", new DateTime(2024, 6, 12, 11, 25, 45, 0, DateTimeKind.Unspecified), "Invoice0000007" },
                    { 8, "8901", 1300.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3726), "testuser", "JPY", "R", new DateTime(2020, 7, 5, 9, 50, 10, 0, DateTimeKind.Unspecified), "Invoice0000008" },
                    { 9, "5670", 450.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3737), "testuser", "INR", "D", new DateTime(2005, 8, 14, 16, 40, 0, 0, DateTimeKind.Unspecified), "Invoice0000009" },
                    { 10, "2341", 1600.00m, new DateTime(2024, 10, 31, 12, 34, 11, 971, DateTimeKind.Utc).AddTicks(3743), "testuser", "CHF", "A", new DateTime(1999, 9, 22, 6, 10, 50, 0, DateTimeKind.Unspecified), "Invoice0000010" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 10, 30, 22, 40, 1, 843, DateTimeKind.Utc).AddTicks(1489));

            migrationBuilder.UpdateData(
                table: "TransactionRecords",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 10, 30, 22, 40, 1, 843, DateTimeKind.Utc).AddTicks(1500));
        }
    }
}
