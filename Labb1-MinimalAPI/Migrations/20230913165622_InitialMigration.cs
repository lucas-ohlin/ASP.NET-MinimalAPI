using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb1_MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLoanAble = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Genre", "IsLoanAble", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("07aaa680-382e-4a9f-a8c5-e9c3d40b2b66"), "Lucas", "Very Spy", "Spy", false, "Bames Jond", new DateTime(2023, 9, 13, 18, 56, 21, 929, DateTimeKind.Local).AddTicks(5927) },
                    { new Guid("973a03e1-d0b0-423b-aab3-a3b4698deaa7"), "Tobias", "Very Spooky", "Spooky", true, "Spooky Book", new DateTime(2023, 9, 13, 18, 56, 21, 929, DateTimeKind.Local).AddTicks(5856) },
                    { new Guid("ee0c8986-7a27-4685-9c4d-05c5fe9f41f4"), "Anas", "More Spookier", "Spooky", true, "Spooky Book 2", new DateTime(2023, 9, 13, 18, 56, 21, 929, DateTimeKind.Local).AddTicks(5922) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
