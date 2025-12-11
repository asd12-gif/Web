namespace Lab5.Models
{
    public class TodoItems
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }
}
