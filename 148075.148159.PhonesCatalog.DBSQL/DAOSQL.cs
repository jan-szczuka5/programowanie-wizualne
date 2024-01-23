using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _148075._148159.PhonesCatalog.Interfaces;
using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.DBSQL.BO;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices.Marshalling;

namespace _148075._148159.PhonesCatalog.DBSQL
{
    public class DAOSQL : DbContext, IDAO
    {
        private IConfiguration _configuration;

        public DAOSQL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>()
                .HasMany(p => (ICollection<Phone>)p.Phones)
                .WithOne(b => (Producer)b.Producer);
        }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
 /*       public string DbPath { get; }

        public DAOSQL()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join("", "phonescatalog.db");
        }

        public DAOSQL(string dbFilePath)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(dbFilePath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"data source={DbPath}");
        }


*/
        public IEnumerable<IProducer> GetAllProducers() => Producers.ToList();
        public IEnumerable<IPhone> GetAllPhones()
        {
            return Phones.Include(phone => phone.Producer).ToList();
        }

        public IProducer CreateNewProducer(int id, string name, string address)
        {
            Producer producer = new Producer() { 
                ID = id, 
                Name = name, 
                Address = address };
            Producers.Add(producer);
            SaveChanges();
            return producer;

        }

        public IPhone CreateNewPhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType)
        {
            Producer? producer = Producers.Find(producerID);
            if (producer == null)
            {
                throw new ArgumentException("That producer doesn't exist");
            }
            Phone phone = new Phone() { 
                ID = id, 
                Name = name, 
                Producer = producer, 
                YearOfProduction = yearOfProduction, 
                AlreadySold = alreadySold, 
                Price = price, 
                SoftwareType = softwareType };
            SaveChanges();
            return phone;
        }

        public void DeleteProducer(int producerId)
        {
            Producer? producer = Producers.Find(producerId);
            if (producer == null)
            {
                Producers.Remove(producer);
                SaveChanges();
            }
        }

        public void DeletePhone(int phoneId)
        {
            Phone? phone = Phones.Find(phoneId);
            if (phone == null)
            {
                Phones.Remove(phone);
                SaveChanges();
            }
        }

        public void UpdateProducer(int id, string name, string address)
        {
            Producer producer = Producers.Find(id);
            if (producer != null)
            {
                producer.ID = id;
                producer.Name = name;
                producer.Address = address;
                SaveChanges();
            };
        }

            public void UpdatePhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType)
        {
            Phone? phone = Phones.Find(id);
            if (phone != null)
            {
                Producer? producer = Producers.Find(producerID);
                if (producer == null)
                {
                    throw new ArgumentException("Publisher doesn't exist");
                }
                phone.ID = id;
                phone.Name = name;
                phone.Producer = producer;
                phone.YearOfProduction = yearOfProduction;
                phone.AlreadySold = alreadySold;
                phone.Price = price;
                phone.SoftwareType = softwareType;
                SaveChanges();
            };
        }

        public IPhone? GetPhone(int phoneId)
        {
            return Phones.Find(phoneId);
        }

        public IProducer? GetProducer(int producerId)
        {
            return Producers.Include(p => p.Phones).FirstOrDefault(p => p.ID == producerId);
        }
    }
}