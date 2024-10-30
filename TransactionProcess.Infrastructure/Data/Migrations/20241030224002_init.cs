using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransactionProcess.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRecords", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TransactionRecords",
                columns: new[] { "Id", "AccountNumber", "Amount", "CreateDate", "CreateUser", "CurrencyCode", "Status", "TransactionDate", "TransactionId" },
                values: new object[,]
                {
                    { 1, "1234", 1000.00m, new DateTime(2024, 10, 30, 22, 40, 1, 843, DateTimeKind.Utc).AddTicks(1489), "testuser", "USD", "A", new DateTime(2024, 2, 20, 12, 33, 16, 0, DateTimeKind.Unspecified), "Invoice0000001" },
                    { 2, "5678", 300.00m, new DateTime(2024, 10, 30, 22, 40, 1, 843, DateTimeKind.Utc).AddTicks(1500), "testuser", "EUR", "R", new DateTime(2024, 2, 21, 2, 4, 59, 0, DateTimeKind.Unspecified), "Invoice0000002" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionRecords");
        }
    }
}
