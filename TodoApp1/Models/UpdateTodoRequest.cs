namespace TodoApp1.Models
{
    public class UpdateTodoRequest
    {
        public string? Email { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
