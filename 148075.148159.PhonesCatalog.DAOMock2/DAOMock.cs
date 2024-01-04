using _148075._148159.PhonesCatalog.Interfaces;

namespace _148075._148159.PhonesCatalog.DAOMock2
{
        public class DAOMock : IDAO
        {
            private List<IProducer> producers;
            private List<IPhone> phones;

            public DAOMock()
            {
                producers = new List<IProducer>()
            {
                new BO.Producer() { ID = 1, Name = "Xiaomi" },
                new BO.Producer() { ID = 2, Name = "Sony" }
            };
                phones = new List<IPhone>()
            {
                new BO.Phone() { ID = 1,
                    Producer = producers[0],
                    Name = "Redmi 13",
                    YeadOfProduction = 2019,
                    Price = 600,
                    AlreadySold = 1163000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 2,
                    Producer = producers[1],
                    Name = "XPERIA xd",
                    YeadOfProduction = 2006,
                    Price = 650,
                    AlreadySold = 1563000,
                    SoftwareType = Core.SoftwareType.Android }
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
