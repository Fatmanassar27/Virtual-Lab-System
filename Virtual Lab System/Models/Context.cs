using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Virtual_Lab_System.Models
{
    public class Context : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }
    }

}
