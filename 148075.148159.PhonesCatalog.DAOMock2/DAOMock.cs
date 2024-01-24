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
                new BO.Producer() { ID = 1, Name = "Xiaomi", Address = "Beijing, China"},
                new BO.Producer() { ID = 2, Name = "Sony", Address = "Tokyo, Japan" },
                new BO.Producer() { ID = 3, Name = "LG", Address = "Seoul, South Korea" },
                new BO.Producer() { ID = 4, Name = "Nokia", Address = "Espoo, Finland" },
                new BO.Producer() { ID = 5, Name = "Asus", Address = "Taipei, Taiwan" }
            };
                phones = new List<IPhone>()
            {
                new BO.Phone() { ID = 1,
                    Producer = producers[0],
                    Name = "Redmi 13",
                    YearOfProduction = 2019,
                    Price = 600,
                    AlreadySold = 1163000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 2,
                    Producer = producers[1],
                    Name = "XPERIA xd",
                    YearOfProduction = 2006,
                    Price = 650,
                    AlreadySold = 1563000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 3,
                    Producer = producers[2],
                    Name = "LG G8",
                    YearOfProduction = 2020,
                    Price = 700,
                    AlreadySold = 800000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 4,
                    Producer = producers[3],
                    Name = "Nokia 9",
                    YearOfProduction = 2019,
                    Price = 750,
                    AlreadySold = 500000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 5,
                    Producer = producers[0],
                    Name = "Redmi Note 10",
                    YearOfProduction = 2021,
                    Price = 800,
                    AlreadySold = 500000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 6,
                    Producer = producers[0],
                    Name = "Xiaomi Mi 11",
                    YearOfProduction = 2021,
                    Price = 900,
                    AlreadySold = 700000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 7,
                    Producer = producers[1],
                    Name = "XPERIA 5 III",
                    YearOfProduction = 2021,
                    Price = 1000,
                    AlreadySold = 300000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 8,
                    Producer = producers[1],
                    Name = "XPERIA 1 II",
                    YearOfProduction = 2020,
                    Price = 950,
                    AlreadySold = 400000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 9,
                    Producer = producers[2],
                    Name = "LG V60 ThinQ",
                    YearOfProduction = 2020,
                    Price = 850,
                    AlreadySold = 200000,
                    SoftwareType = Core.SoftwareType.Android },
                new BO.Phone() { ID = 10,
                    Producer = producers[2],
                    Name = "LG Velvet",
                    YearOfProduction = 2020,
                    Price = 750,
                    AlreadySold = 300000,
                    SoftwareType = Core.SoftwareType.Android },
            };
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

        public void CreatePhone(IPhone phone)
        {
            throw new NotImplementedException();
        }

        public void DeletePhone(int phoneId)
        {
            IPhone phoneDelete = phones.First(phone => phone.ID.Equals(phoneId));
            phones.Remove(phoneDelete);
        }

        public void DeleteProducer(int producerId)
        {
            IProducer producerDelete = producers.First(producer => producer.ID.Equals(producerId));
            IEnumerable<IPhone> phonesToDelete = phones.Where(phone => phone.Producer.ID.Equals(producerId));
            foreach(IPhone phone in phonesToDelete) 
            {
                phones.Remove(phone);
            }
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

        public IPhone NewPhone()
        {
            throw new NotImplementedException();
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
