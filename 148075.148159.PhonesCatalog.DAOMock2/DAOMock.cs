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
                new BO.Producer() { ID = 1, Name = "Xiaomi"},
                new BO.Producer() { ID = 2, Name = "Sony" }
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
                    SoftwareType = Core.SoftwareType.Android }
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
