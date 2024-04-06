using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskAbdallahRiyad.Models;

namespace TaskAbdallahRiyad.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet <Contact> contacts { get; set; }    
    }
}
