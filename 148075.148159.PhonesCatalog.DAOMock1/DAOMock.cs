using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.DAOMock1
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers;
        private List<IPhone> phones;

        public DAOMock()
        {
            producers = new List<IProducer>();
            CreateNewProducer(new BO.Producer() { ID = 1, Name = "Samsung" });
            CreateNewProducer(new BO.Producer() { ID = 2, Name = "Apple" });

            phones = new List<IPhone>();
            CreateNewPhone(new BO.Phone()
            {
                ID = 1,
                Producer = producers[0],
                Name = "Galaxy S23",
                YearOfProduction = 2022,
                Price = 600,
                AlreadySold = 1163000,
                SoftwareType = Core.SoftwareType.Android
            });
            CreateNewPhone(new BO.Phone()
            {
                ID = 2,
                Producer = producers[1],
                Name = "IPhone 15",
                YearOfProduction = 2023,
                Price = 650,
                AlreadySold = 1563000,
                SoftwareType = Core.SoftwareType.iOS
            });
        }
        public IPhone CreateNewPhone(IPhone phone)
        {
            phones.Add(phone);
            return phone;
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
            producers.Remove(producerDelete);
        }

        public IEnumerable<IPhone> GetAllPhones()
        {
            return phones;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public void UpdatePhone(IPhone phoneUpdated)
        {
            int phoneId = phones.FindIndex(phoneOld => phoneOld.ID.Equals(phoneUpdated.ID));
            phones[phoneId] = phoneUpdated;
        }

        public void UpdateProducer(IProducer producerUpdated)
        {
            int producerId = producers.FindIndex(producerOld => producerOld.ID.Equals(producerUpdated.ID));
            producers[producerId] = producerUpdated;
        }
    }
}
