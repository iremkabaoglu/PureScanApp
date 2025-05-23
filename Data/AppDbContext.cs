using Microsoft.EntityFrameworkCore;
using PureScanApp.Models;
using System.Collections.Generic;

namespace PureScanApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
