using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab8.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; } 
        public string? Description { get; set; }
        public int CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}
