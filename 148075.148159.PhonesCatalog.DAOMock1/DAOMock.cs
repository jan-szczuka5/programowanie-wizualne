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
            producers = new List<IProducer>()
            {
                new BO.Producer() { ID = 1, Name = "Samsung" },
                new BO.Producer() { ID = 2, Name = "Apple" }
            };
            phones = new List<IPhone>()
            {
                new BO.Phone() { ID = 1,
                    Producer = producers[0],
                    Name = "Galaxy S23",
                    YeadOfProduction = 2022,
                    Price = 600,
                    AlreadySold = 1163000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 2,
                    Producer = producers[1],
                    Name = "IPhone 15",
                    YeadOfProduction = 2023,
                    Price = 650,
                    AlreadySold = 1563000,
                    SoftwareType = Core.SoftwareType.iOS }
            };
        }
        public IPhone CreateNewPhone()
        {
            return new BO.Phone();
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IEnumerable<IPhone> GetAllPhones()
        {
            return phones;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }
    }
}
