namespace TodoApp1.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
