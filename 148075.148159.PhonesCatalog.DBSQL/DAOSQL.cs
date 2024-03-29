﻿using Microsoft.EntityFrameworkCore;
using _148075._148159.PhonesCatalog.Interfaces;
using Microsoft.Extensions.Configuration;

namespace _148075._148159.PhonesCatalog.DBSQL
{
    public class DAOSQL : DbContext, IDAO
    {
        public DbSet<PhoneDBSQL> Phones { get; set; }
        public DbSet<ProducerDBSQL> Producers { get; set; }
        public string DbPath { get; }

        private IConfiguration _configuration;

        public DAOSQL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string projectRootDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.Parent?.FullName;

            if (projectRootDirectory != null)
            {

                string dbFilePath = Path.Combine(projectRootDirectory, "phonescatalog.db");
                options.UseSqlite($"Data Source={dbFilePath}");
            }
            else
            {
                throw new InvalidOperationException("Failed to determine the project's root directory.");
            }
        }



        public IEnumerable<IProducer> GetAllProducers() => Producers.Select(producer => producer.ToIProducer());
        public IEnumerable<IPhone> GetAllPhones()
        {
            return Phones.Select(phone => phone.ToIPhone(Producers.ToList()));
        }

        public IProducer CreateNewProducer(IProducer producer)
        {
            Add(new ProducerDBSQL() { ID = producer.ID, Name = producer.Name, Address = producer.Address });
            SaveChanges();
            return producer;

        }

        public IPhone CreateNewPhone(IPhone phone)
        {
            Add(new PhoneDBSQL() { ID = phone.ID, Name = phone.Name, ProducerID = phone.Producer.ID, YearOfProduction = phone.YearOfProduction, AlreadySold = phone.AlreadySold, Price = phone.Price, SoftwareType = phone.SoftwareType });
            SaveChanges();
            return phone;
        }

        public void DeleteProducer(int producerId)
        {
            var producer = Producers.FirstOrDefault(producer => producer.ID == producerId);
            Remove(producer);
            SaveChanges();
        }

        public void DeletePhone(int phoneId)
        {
            var phone = Phones.FirstOrDefault(phone => phone.ID == phoneId);
            Remove(phone);
            SaveChanges();
        }

        public void UpdateProducer(IProducer producerUpdated)
        {
            var producer = Producers.FirstOrDefault(producer => producer.ID.Equals(producerUpdated.ID));
            producer.Name = producerUpdated.Name;
            producer.Address = producerUpdated.Address;

            Entry(producer).CurrentValues.SetValues(producer);
        }

        public void UpdatePhone(IPhone phoneUpdated)
        {
            var phone = Phones.FirstOrDefault(phone => phone.ID.Equals(phoneUpdated.ID));
            phone.Name = phoneUpdated.Name;
            phone.ProducerID = phoneUpdated.Producer.ID;
            phone.YearOfProduction = phoneUpdated.YearOfProduction;
            phone.AlreadySold = phoneUpdated.AlreadySold;
            phone.Price = phoneUpdated.Price;
            phone.SoftwareType = phoneUpdated.SoftwareType;

            Entry(phone).CurrentValues.SetValues(phone);
        }

        public IPhone NewPhone()
        {
            throw new NotImplementedException();
        }

        public void CreatePhone(IPhone phone)
        {
            throw new NotImplementedException();
        }
    }
}