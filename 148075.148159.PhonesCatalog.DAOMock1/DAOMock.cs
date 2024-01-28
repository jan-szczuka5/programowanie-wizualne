using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _148075._148159.PhonesCatalog.DAOMock1
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers;
        private List<IPhone> phones;
        private int nextPhoneId = 3;

        public DAOMock()
        {
            producers = new List<IProducer>()
            {
                new BO.Producer() { ID = 1, Name = "Samsung", Address = "Seoul, South Korea" },
                new BO.Producer() { ID = 2, Name = "Apple", Address = "California, United States" },
                new BO.Producer() { ID = 3, Name = "Huawei", Address = "Shenzhen, China" },
                new BO.Producer() { ID = 4, Name = "Google", Address = "Mountain View, United States" },
                new BO.Producer() { ID = 5, Name = "OnePlus", Address = "Shenzhen, China" },
                new BO.Producer() { ID = 6, Name = "Motorola", Address = "Chicago, United States" }
            };

            phones = new List<IPhone>()
            {
                new BO.Phone()
                {
                    ID = 1,
                    Producer = producers[0],
                    Name = "Galaxy S23",
                    YearOfProduction = 2022,
                    Price = 600,
                    AlreadySold = 1163000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 2,
                    Producer = producers[1],
                    Name = "IPhone 15",
                    YearOfProduction = 2023,
                    Price = 650,
                    AlreadySold = 1563000,
                    SoftwareType = Core.SoftwareType.iOS
                },
                new BO.Phone()
                {
                    ID = 3,
                    Producer = producers[2],
                    Name = "Mate 40",
                    YearOfProduction = 2022,
                    Price = 550,
                    AlreadySold = 800000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 4,
                    Producer = producers[3],
                    Name = "Pixel 6",
                    YearOfProduction = 2023,
                    Price = 700,
                    AlreadySold = 500000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 5,
                    Producer = producers[4],
                    Name = "OnePlus 9",
                    YearOfProduction = 2021,
                    Price = 650,
                    AlreadySold = 1200000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 6,
                    Producer = producers[5],
                    Name = "Moto G Power",
                    YearOfProduction = 2020,
                    Price = 300,
                    AlreadySold = 900000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 7,
                    Producer = producers[0],
                    Name = "Galaxy Note 20",
                    YearOfProduction = 2021,
                    Price = 800,
                    AlreadySold = 1500000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 8,
                    Producer = producers[1],
                    Name = "IPhone SE",
                    YearOfProduction = 2020,
                    Price = 400,
                    AlreadySold = 2000000,
                    SoftwareType = Core.SoftwareType.iOS
                },
                new BO.Phone()
                {
                    ID = 9,
                    Producer = producers[2],
                    Name = "Huawei P50",
                    YearOfProduction = 2022,
                    Price = 700,
                    AlreadySold = 900000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 10,
                    Producer = producers[3],
                    Name = "Google Pixel 5",
                    YearOfProduction = 2020,
                    Price = 600,
                    AlreadySold = 1000000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 11,
                    Producer = producers[4],
                    Name = "OnePlus 8T",
                    YearOfProduction = 2020,
                    Price = 550,
                    AlreadySold = 1200000,
                    SoftwareType = Core.SoftwareType.Android
                },
                new BO.Phone()
                {
                    ID = 12,
                    Producer = producers[5],
                    Name = "Motorola Edge",
                    YearOfProduction = 2021,
                    Price = 450,
                    AlreadySold = 800000,
                    SoftwareType = Core.SoftwareType.Android
                }
            };
        }
        public IPhone CreateNewPhone(IPhone phone)
        {
            phone.ID = nextPhoneId++;
            Console.WriteLine("in daomock");
            phones.Add(phone);
            Console.WriteLine(phone.Name);
            return phone;
        }

        public void CreatePhone(IPhone phone)
        {
            var phone_element = new BO.Phone()
            {
                ID = phones.Max(f => f.ID) + 1,
                Name = phone.Name,
                Producer = phone.Producer,
                YearOfProduction = phone.YearOfProduction,
                AlreadySold = phone.AlreadySold,
                Price = phone.Price,
                SoftwareType = phone.SoftwareType
            };
            Console.WriteLine("in daomock");
            phones.Add(phone_element);
            Console.WriteLine(phone.Name);
        }

        public IProducer CreateNewProducer(IProducer producer)
        {
            producer.ID = producers.Max(f => f.ID) + 1;
            producers.Add(producer);
            return producer;
        }



        public void DeletePhone(int phoneId)
        {
            IPhone phoneDelete = phones.First(phone => phone.ID.Equals(phoneId));
            phones.Remove(phoneDelete);
        }

        public void DeleteProducer(int producerId)
        {
            IProducer producerDelete = producers.First(producer => producer.ID.Equals(producerId));
            List<IPhone> phonesToDelete = phones.Where(phone => phone.Producer.ID.Equals(producerId)).ToList();
            foreach (IPhone phone in phonesToDelete)
            {
                phones.Remove(phone);
            }
            producers.Remove(producerDelete);
        }

        public IEnumerable<IPhone> GetAllPhones()
        {
            Console.WriteLine("daomock getallphones");
            return phones;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public IPhone NewPhone()
        {
            throw new NotImplementedException();
        }

        public void UpdatePhone(IPhone phoneUpdated)
        {
            int phoneIndex = phones.FindIndex(phone => phone.ID == phoneUpdated.ID);
            if (phoneIndex != -1)
            {
                phones[phoneIndex] = phoneUpdated; // Maintain the existing ID
            }
            else
            {
                throw new Exception("Phone not found");
            }
        }

        public void UpdateProducer(IProducer producerUpdated)
        {
            int producerId = producers.FindIndex(producerOld => producerOld.ID.Equals(producerUpdated.ID));
            producers[producerId] = producerUpdated;
        }
    }
}
