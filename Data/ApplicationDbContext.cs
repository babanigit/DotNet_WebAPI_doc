using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet_WebAPI_doc.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet_WebAPI_doc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}