using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "All tech gadgets and devices", "Technology" },
                    { 2, "Trendy clothes and accessories", "Fashion" },
                    { 3, "Novels, study materials, and office items", "Books & Stationery" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "NewPrice", "OldPrice" },
                values: new object[,]
                {
                    { 1, 1, "Powerful laptop with 16GB RAM and 512GB SSD", "Laptop Pro X14", 1499.99m, 1699.99m },
                    { 2, 1, "Portable speaker with deep bass and 10h battery life", "Bluetooth Speaker Mini", 79.99m, 99.99m },
                    { 3, 2, "Unisex denim jacket, perfect for winter style", "Denim Jacket", 59.99m, 79.99m },
                    { 4, 3, "Pack of 3 ruled notebooks with hardcover design", "Office Notebook Set", 24.99m, 29.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
