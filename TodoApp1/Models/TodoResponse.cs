namespace TodoApp1.Models
{
    public class TodoResponse
    {
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public int Pages { get; set; }
        public int CurrentPage{ get; set; } 


    }
}
