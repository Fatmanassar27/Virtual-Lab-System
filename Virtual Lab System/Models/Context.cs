using Microsoft.EntityFrameworkCore;

namespace Virtual_Lab_System.Models
{
    public class Context : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }
    }

}
