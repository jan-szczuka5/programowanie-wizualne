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
                new BO.Producer() { ID = 2, Name = "Apple", Address = "California, United States" }
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
