using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.DAOMock1.BO;
using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace _148075._148159.PhonesCatalog.DAOMock1
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers;
        private List<IPhone> phones;

        public DAOMock()
        {
            producers = new List<IProducer>();
            var producer1 = new Producer
            {
                ID = 1,
                Name = "Samsung",
                Address = "Seoul, South Korea"
            };
            producers.Add(producer1);

            var producer2 = new Producer
            {
                ID = 2,
                Name = "Apple",
                Address = "California, United States",
            };
            producers.Add(producer2);

            phones = new List<IPhone>();

            var phone1 = new Phone
            {
                ID = 1,
                Producer = producers[0],
                Name = "Galaxy S23",
                YearOfProduction = 2022,
                Price = 600,
                AlreadySold = 1163000,
                SoftwareType = SoftwareType.Android,
            };

            phones.Add(phone1);

            var phone2 = new Phone
            {
                ID = 2,
                Producer = producers[1],
                Name = "iPhone 15",
                YearOfProduction = 2023,
                Price = 650,
                AlreadySold = 1563000,
                SoftwareType = SoftwareType.iOS,
            };


            phones.Add(phone2);

        }


        public IPhone CreateNewPhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType)
        {
            Producer? producer = (Producer?)GetProducer(producerID);
            if (producer == null)
            {
                throw new ArgumentException("That producer doesn't exist");
            }
            Phone phone = new Phone
            {
                ID = id,
                Name = name,
                Producer = producer,
                YearOfProduction = yearOfProduction,
                AlreadySold = alreadySold,
                Price = price,
                SoftwareType = softwareType
            };
            phones.Add(phone);
            return phone;
        }

        public IProducer CreateNewProducer(int id, string name, string address)
        {
            var producer = new Producer
            {
                ID = id,
                Name = name,
                Address = address,
                Phones = new List<IPhone>()
            };
            producers.Add(producer);
            return producer;
        }


        public void DeletePhone(int phoneId)
        {
            int i;
            for (i = 0; i < phones.Count(); i++)
            {
                if (phones[i].ID == phoneId)
                {
                    break;
                }
            }
            phones.RemoveAt(i);
        }

        public void DeleteProducer(int producerId)
        {
            int i;
            for (i = 0; i < producers.Count(); i++)
            {
                if (producers[i].ID.Equals(producerId))
                {
                    break;
                }
            }
            phones.RemoveAll(book => book.Producer.ID == producers[i].ID);
            producers.RemoveAt(i);
        }

        public IEnumerable<IPhone> GetAllPhones()
        {
            return phones;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public IPhone? GetPhone(int phoneId)
        {
            for (int i = 0; i < phones.Count(); i++)
            {
                if (phones[i].ID == phoneId)
                {
                    return phones[i];
                }
            }
            return null;
        }

        public IProducer? GetProducer(int producerId)
        {
            for (int i = 0; i < producers.Count(); i++)
            {
                if (producers[i].ID == producerId)
                {
                    return producers[i];
                }
            }
            return null;
        }

        public void UpdatePhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType)
        {
            int i;
            for (i = 0; i < phones.Count(); i++)
            {
                if (phones[i].ID == id)
                {
                    break;
                }
            }
            Producer? producer = (Producer?)GetProducer(producerID);
            if (producer == null)
            {
                throw new ArgumentException("Producer doesn't exist");
            }
            phones[i].Name = name;
            phones[i].Producer = producer;
            phones[i].YearOfProduction = yearOfProduction;
            phones[i].AlreadySold = alreadySold;
            phones[i].Price = price;
            phones[i].SoftwareType = softwareType;
        }


        public void UpdateProducer(int id, string name, string address)
        {
            int i;
            for (i = 0; i < producers.Count(); i++)
            {
                if (producers[i].ID.Equals(id))
                {
                    break;
                }
            }
            producers[i].Name = name;
            producers[i].Address = address;
        }
    }
}
