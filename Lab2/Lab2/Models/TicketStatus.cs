using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public enum TicketStatus
    {
        [Display(Name = "New")]
        New,

        [Display(Name = "Inprogress")]
        InProgress,

        [Display(Name = "Closed")]
        Closed
    }
}