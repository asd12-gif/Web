using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Lab2.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Fullname")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "PhoneNumber")]
        public string? PhoneNumber { get; set; }

        // Navigation Property
        public ICollection<Ticket>? Tickets { get; set; }
    }
}