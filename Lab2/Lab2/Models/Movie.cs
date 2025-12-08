using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Thiếu namespace này trong code bạn dán, nhưng nó cần thiết cho [Column(TypeName...)]

namespace Lab2.Models
{
    public class Movie
    {
        public int Id { get; set; }

        // Tên phim (Bắt buộc, độ dài tối đa 60, tối thiểu 3)
        [Required(ErrorMessage = "Title is required")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        // Ngày phát hành
        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        // Giá
        [Required]
        [Range(1, 1000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // Rating
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        [Required]
        public string Rating { get; set; } = string.Empty;

        // CategoryId (Khóa ngoại)
        [Display(Name = "Category")]
        [Required]
        public int? CategoryId { get; set; } 

        //Navigation Property
        public Category? Category { get; set; }
    }
}