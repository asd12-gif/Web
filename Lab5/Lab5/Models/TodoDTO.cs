namespace Lab5.Models
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }

        public TodoDTO() { }
        public TodoDTO(TodoItems todoItem)
        {
            Id = todoItem.Id;
            Name = todoItem.Name;
            IsComplete = todoItem.IsComplete;
        }
    }
}
