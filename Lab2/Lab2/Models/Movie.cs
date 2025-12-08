using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Movie
    {
        public int Id { get; set; }

        // Tên phim
        public string Title { get; set; } = string.Empty;

        // Ngày phát hành 
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        // Thể loại
        public string Genre { get; set; } = string.Empty;

        // Giá 
        public decimal Price { get; set; }

        [Display(Name = "Category")] 
        public int? CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }
    }
}
