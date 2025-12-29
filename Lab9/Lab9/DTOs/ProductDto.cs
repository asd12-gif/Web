namespace Lab9.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!; 
    }
}