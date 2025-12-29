using System.ComponentModel.DataAnnotations.Schema;

namespace lab8.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
       
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public string? Note { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
