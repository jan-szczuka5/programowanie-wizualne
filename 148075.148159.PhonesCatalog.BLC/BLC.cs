using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace _148075._148159.PhonesCatalog.BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC(IConfiguration configuration)
        {
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DBLibraryName"]!;
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
            Type? typeToCreate = null;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = type;
                    break;
                }
            }
            ConstructorInfo? constructor = typeToCreate!.GetConstructor(new[] { typeof(IConfiguration) });
            if (constructor != null)
            {
                dao = (IDAO)constructor.Invoke(new object[] { configuration })!;
            }
            else
            {
                dao = (IDAO)Activator.CreateInstance(typeToCreate!, null)!;
            }
        }

        public BLC(IDAO dao)
        {
            this.dao = dao;
        }

        /*public BLC(string path)
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
*/
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

        public IProducer GetProducerById(int producerId)
        {
            return dao.GetProducer(producerId);
        }
        public IPhone GetPhoneById(int phoneId)
        {
            return dao.GetPhone(phoneId);
        }

        public IEnumerable<string> GetAllPhonesNames() => from phone in dao.GetAllProducers() select phone.Name;

        public IEnumerable<string> GetAllProducersNames() => from producer in dao.GetAllProducers() select producer.Name;

        public void CreatePhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType)
        {
            dao.CreateNewPhone(id, name, producerID, yearOfProduction, alreadySold, price, softwareType);
        }

        public void CreateProducer(int id, string name, string address)
        {
            dao.CreateNewProducer(id, name, address);
        }

        public void DeleteProducer(int producerId)
        {
            dao.DeleteProducer(producerId);
        }

        public void DeletePhone(int phoneId)
        {
            dao.DeletePhone(phoneId);
        }

        public void UpdateProducer(int id, string name, string address)
        {
            dao.UpdateProducer(id, name, address);
        }

        public void UpdatePhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType)
        {
            dao.UpdatePhone(id, name, producerID, yearOfProduction, alreadySold, price, softwareType);
        }

        public IEnumerable<IPhone> FilterPhoneByProducer(string producerName)
        {
            return dao.GetAllPhones().Where(phone => phone.Producer.Name.Equals(producerName));
        }

        public IEnumerable<IPhone> FilterPhoneBySoftwareType(SoftwareType softwareType)
        {
            return dao.GetAllPhones().Where(phone => phone.SoftwareType.Equals(softwareType));
        }

        public IEnumerable<IPhone> FilterPhoneByYear(int yearOfProduction)
        {
            return dao.GetAllPhones().Where(phone => phone.YearOfProduction.Equals(yearOfProduction));
        }
        public IEnumerable<IProducer> FilterProducerByAddress(string address)
        {
            return dao.GetAllProducers().Where(producer => producer.Address.Equals(address));
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