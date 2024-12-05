using Microsoft.EntityFrameworkCore;
using DotNet9CookieAuthAPI.Models;

namespace DotNet9CookieAuthAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
