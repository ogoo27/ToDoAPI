using Microsoft.EntityFrameworkCore;
using TodoApp1.Models;

namespace TodoApp1.Data
{
    public class TodosAPIDbContext : DbContext
    {
        public TodosAPIDbContext(DbContextOptions options) : base(options)
        {
        }

   

       public DbSet<Todo> Todos { get; set; }
    }
}
