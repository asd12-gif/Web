using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab9.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToExternalImageLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Điện thoại" },
                    { 2, "Laptop" },
                    { 3, "Phụ kiện" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "https://cdn.tgdd.vn/Products/Images/42/305658/iphone-15-pro-blue-thumb-600x600.jpg", "iPhone 15 Pro", 25000000m },
                    { 2, 1, "https://cdn.tgdd.vn/Products/Images/42/307174/samsung-galaxy-s24-ultra-grey-thumb-600x600.jpg", "Samsung Galaxy S24 Ultra", 30000000m },
                    { 3, 2, "https://cdn.tgdd.vn/Products/Images/44/322631/apple-macbook-air-m3-13-inch-8gb-256gb-thumb-600x600.jpg", "MacBook Air M3", 27000000m },
                    { 4, 2, "https://cdn.tgdd.vn/Products/Images/44/322442/asus-rog-strix-scar-18-g834jyr-i9-n4053w-thumb-600x600.jpg", "Laptop ASUS ROG Strix", 35000000m },
                    { 5, 3, "https://cdn.tgdd.vn/Products/Images/54/289700/airpods-pro-2-thumb-600x600.jpg", "AirPods Pro Gen 2", 5900000m },
                    { 6, 3, "https://cdn.tgdd.vn/Products/Images/2162/285077/marshall-emberton-ii-thumb-600x600.jpg", "Loa Marshall Emberton II", 4500000m },
                    { 7, 1, "https://cdn.tgdd.vn/Products/Images/522/294101/ipad-pro-m2-11-wifi-xam-thumb-600x600.jpg", "iPad Pro M2", 21000000m },
                    { 8, 2, "https://cdn.tgdd.vn/Products/Images/44/313333/dell-inspiron-15-3520-i5-71021696-thumb-600x600.jpg", "Laptop Dell Inspiron 15", 18000000m },
                    { 9, 3, "https://cdn.tgdd.vn/Products/Images/7077/314227/apple-watch-s9-41mm-vien-nhom-day-cao-su-hong-thumb-600x600.jpg", "Apple Watch Series 9", 9500000m },
                    { 10, 3, "https://cdn.tgdd.vn/Products/Images/75/203498/chuột-gaming-logitech-g502-hero-1-600x600.jpg", "Chuột Logitech G502", 1200000m }
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
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

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
