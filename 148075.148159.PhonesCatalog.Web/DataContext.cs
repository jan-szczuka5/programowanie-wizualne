using Microsoft.EntityFrameworkCore;
using _148075._148159.PhonesCatalog.Web.Models;

namespace _148075._148159.PhonesCatalog.Web
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }

        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
    }
}
