using Microsoft.AspNetCore.Http;

namespace Lab9.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        // Dùng để nhận file upload từ máy tính
        public IFormFile? Image { get; set; }

        // Dùng để nhận link ảnh URL (copy từ mạng)
        public string? ImageUrl { get; set; }
    }
}