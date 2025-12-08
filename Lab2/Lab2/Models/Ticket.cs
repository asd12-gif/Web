using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Display(Name = "Datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Status")]
        public TicketStatus Status { get; set; } = TicketStatus.New;
        public string SeatNumber { get; set; } = string.Empty;

        // --- Mối quan hệ với CUSTOMER ---
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // --- MỐI QUAN HỆ VỚI MOVIE ---
        [Display(Name = "Movie")]
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
