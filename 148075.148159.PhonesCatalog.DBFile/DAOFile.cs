using _148075._148159.PhonesCatalog.Interfaces;

namespace _148075._148159.PhonesCatalog.DBFile
{
    public class DAOFile : IDAO
    {
        private List<IProducer> producers;
        private List<IPhone> phones;


        public DAOFile()
        {
            producers = new List<IProducer>()
            {
                new ProducerDBFile() { ID = 1, Name = "Samsung" },
                new ProducerDBFile() { ID = 2, Name = "Apple" }
            };
            SaveProducers();

            phones = new List<IPhone>()
            {
                new PhoneDBFile() { ID = 1,
                    Producer = producers[0],
                    Name = "Galaxy S23",
                    YearOfProduction = 2022,
                    Price = 600,
                    AlreadySold = 1163000,
                    SoftwareType = Core.SoftwareType.Android },
                new PhoneDBFile() { ID = 2,
                    Producer = producers[1],
                    Name = "IPhone 15",
                    YearOfProduction = 2023,
                    Price = 650,
                    AlreadySold = 1563000,
                    SoftwareType = Core.SoftwareType.iOS }
            };
            SavePhones();
        }

        public DAOFile(string producersDBFile, string phonesDBFile)
        {
            phones = Serializer.Deserialize<IPhone>(phonesDBFile);
            producers = Serializer.Deserialize<IProducer>(producersDBFile);
        }

        private void SaveProducers()
        {
            Serializer.Serialize("phones.bin", phones);
        }

        private void SavePhones()
        {
            Serializer.Serialize("producers.bin", producers);
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public IEnumerable<IPhone> GetAllPhones()
        {
            return phones;
        }

        public IProducer CreateNewProducer(IProducer producer)
        {
            producers.Add(producer);
            SaveProducers();
            return producer;
        }

        public IPhone CreateNewPhone(IPhone phone)
        {
            phones.Add(phone);
            SavePhones();
            return phone;
        }

        public void DeleteProducer(int producerId)
        {
            IProducer producerDelete = producers.First(producer => producer.ID.Equals(producerId));
            producers.Remove(producerDelete);
            SaveProducers();
        }

        public void DeletePhone(int phoneId)
        {
            IPhone phoneDelete = phones.First(phone => phone.ID.Equals(phoneId));
            phones.Remove(phoneDelete);
            SavePhones();
        }

        public void UpdateProducer(IProducer producer)
        {
            int producerId = producers.FindIndex(producerOld => producerOld.ID.Equals(producer.ID));
            producers[producerId] = producer;
            SaveProducers();
        }

        public void UpdatePhone(IPhone phone)
        {
            int phoneId = phones.FindIndex(phoneOld => phoneOld.ID.Equals(phone.ID));
            phones[phoneId] = phone;
            SavePhones();
        }

        public void CreatePhone(IPhone phone)
        {
            throw new NotImplementedException();
        }

        public IPhone NewPhone()
        {
            throw new NotImplementedException();
        }
    }
}