namespace lab8.Models
{
    public class CarModelVm
    {
        public int Id { get; set; }
        public string CarModelName { get; set; } = null!;
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? ImageUrl { get; set; } 
        public string? Description { get; set; }
    }
}
