using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Todolist> todolist {get; set; }
    }
}