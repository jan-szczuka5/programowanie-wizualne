using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;
using _148075._148159.PhonesCatalog.DBSQL;
using System.Reflection;

namespace _148075._148159.PhonesCatalog.BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC(string path)
        {
            LoadDatasource(path);
        }

        public void LoadDatasource(string path)
        {
            if (path.EndsWith(".dll"))
                LoadLibrary(path);
            else if (path.EndsWith(".db"))
                LoadSql(path);
        }
        public void LoadSql(string path)
        {
            dao = new DAOSQL(path);
        }

        public void LoadLibrary(string path)
        {
            var typeToCreate = FindDAOType(path);

            if (typeToCreate != null)
            {
                dao = CreateDAOInstance(typeToCreate);
            }
            else
            {
                throw new InvalidOperationException("No compatible IDAO type found in assembly.");
            }
        }

        #region Handle DAO 
        private Type FindDAOType(string path)
        {
            try
            {
                var assembly = Assembly.UnsafeLoadFrom(path);
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IDAO).IsAssignableFrom(type))
                    {
                        return type;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load assembly or find IDAO: " + ex.Message);
                throw;
            }

            return null;
        }

        private IDAO CreateDAOInstance(Type daoType)
        {
            try
            {
                return (IDAO)Activator.CreateInstance(daoType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create instance of IDAO: {daoType.Name}\n{ex.Message}");
                throw;
            }
        }
        #endregion



        public IEnumerable<IProducer> GetProducers()
        {
            return dao.GetAllProducers();
        }
        public IEnumerable<IPhone> GetPhones()
        {
            return dao.GetAllPhones();
        }

        public IEnumerable<IProducer> GetProducerById(int producerId)
        {
            return dao.GetAllProducers().Where(producer => producer.ID.Equals(producerId));
        }
        public IEnumerable<IPhone> GetPhoneById(int phoneId)
        {
            return dao.GetAllPhones().Where(phone => phone.ID.Equals(phoneId));
        }

        public void CreatePhone(IPhone phone)
        {
            dao.CreateNewPhone(phone);
        }

        public void CreateProducer(IProducer producer)
        {
            dao.CreateNewProducer(producer);
        }

        public void DeleteProducer(int producerId)
        {
            dao.DeleteProducer(producerId);
        }

        public void DeletePhone(int phoneId)
        {
            dao.DeletePhone(phoneId);
        }

        public void UpdateProducer(IProducer producer)
        {
            dao.UpdateProducer(producer);
        }

        public void UpdatePhone(IPhone phone)
        {
            dao.UpdatePhone(phone);
        }

        public IEnumerable<IPhone> FilterPhoneByProducer(string producerName)
        {
            return dao.GetAllPhones().Where(phone => phone.Producer.Name.Equals(producerName));
        }

        public IEnumerable<IPhone> FilterPhoneBySoftwareType(SoftwareType softwareType)
        {
            return dao.GetAllPhones().Where(phone => phone.SoftwareType.Equals(softwareType));
        }

        public IEnumerable<IPhone> SearchPhoneByName(string phoneName)
        {
            return dao.GetAllPhones().Where(phone => phone.Name.Contains(phoneName));
        }

        public IEnumerable<IProducer> SearchProducerByName(string producerName)
        {
            return dao.GetAllProducers().Where(producer => producer.Name.Contains(producerName));
        }


    }
}