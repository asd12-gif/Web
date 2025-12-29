namespace lab8.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Country { get; set; }
        public string? ImageUrl { get; set; } 
        public string? Description { get; set; }
        public ICollection<CarModel>? CarModels { get; set; }

    }
}
