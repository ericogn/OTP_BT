using Microsoft.EntityFrameworkCore;
using OTP_server.Models;

namespace OTP_server.Context
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration? _configuration;
        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("OTP_Database"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
