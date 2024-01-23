using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using _148075._148159.PhonesCatalog.DBSQL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.DBSQL
{
    internal class DataContext:DbContext
    {
        private IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }
        DbSet<Phone> Phones { get; set; }
        DbSet<Producer> Producers { get; set; }

    }
}
