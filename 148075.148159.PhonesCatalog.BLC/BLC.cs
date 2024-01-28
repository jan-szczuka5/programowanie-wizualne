using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;
using _148075._148159.PhonesCatalog.DBSQL;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace _148075._148159.PhonesCatalog.BLC
{
    public class BLC
    {
        private IDAO _dao;
        private static BLC instance;
        private static readonly object lockObject = new object();
        public BLC(IConfiguration configuration)
        {
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"]!;
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
                _dao = (IDAO)constructor.Invoke(new object[] { configuration })!;
            }
            else
            {
                _dao = (IDAO)Activator.CreateInstance(typeToCreate!, null)!;
            }
        }

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
            _dao = new DAOSQL(path);
        }

        public void LoadLibrary(string path)
        {
            var typeToCreate = FindDAOType(path);

            if (typeToCreate != null)
            {
                _dao = CreateDAOInstance(typeToCreate);
            }
            else
            {
                throw new InvalidOperationException("No compatible IDAO type found in assembly.");
            }
        }

        public static BLC GetInstance(string libraryName)
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new BLC(libraryName);
                    }
                }
            }

            return instance;
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

        public IPhone NewPhone()
        {             
            return _dao.NewPhone();
        }

        public IEnumerable<IProducer> GetProducers()
        {
            return _dao.GetAllProducers();
        }
        public IEnumerable<IPhone> GetPhones()
        {
            return _dao.GetAllPhones();
        }

        public IProducer GetProducerById(int producerId)
        {
            return _dao.GetAllProducers().First(producer => producer.ID.Equals(producerId));
        }
        public IPhone GetPhoneById(int phoneId)
        {
            return _dao.GetAllPhones().First(phone => phone.ID.Equals(phoneId));
        }

        public IEnumerable<string> GetAllPhonesNames() => from phone in _dao.GetAllProducers() select phone.Name;

        public IEnumerable<string> GetAllProducersNames() => from producer in _dao.GetAllProducers() select producer.Name;

        public void CreatePhone(IPhone phone)
        {
            _dao.CreateNewPhone(phone);
        }

        public void CreateNewPhone(IPhone phone)
        {
            _dao.CreatePhone(phone);
        }

        public void CreateProducer(IProducer producer)
        {
            _dao.CreateNewProducer(producer);
        }

        public void DeleteProducer(int producerId)
        {
            _dao.DeleteProducer(producerId);
        }

        public void DeletePhone(int phoneId)
        {
            _dao.DeletePhone(phoneId);
        }

        public void UpdateProducer(IProducer producer)
        {
            _dao.UpdateProducer(producer);
        }

        public void UpdatePhone(IPhone phone)
        {
            _dao.UpdatePhone(phone);
        }

        public IEnumerable<IPhone> FilterPhoneByProducer(string producerName)
        {
            return _dao.GetAllPhones().Where(phone => phone.Producer.Name.Equals(producerName));
        }

        public IEnumerable<IPhone> FilterPhoneBySoftwareType(SoftwareType softwareType)
        {
            return _dao.GetAllPhones().Where(phone => phone.SoftwareType.Equals(softwareType));
        }

        public IEnumerable<IPhone> FilterPhoneByYear(int yearOfProduction)
        {
            return _dao.GetAllPhones().Where(phone => phone.YearOfProduction.Equals(yearOfProduction));
        }
        public IEnumerable<IProducer> FilterProducerByAddress(string address)
        {
            return _dao.GetAllProducers().Where(producer => producer.Address.Equals(address));
        }
        public IEnumerable<IPhone> SearchPhoneByName(string phoneName)
        {
            return _dao.GetAllPhones().Where(phone => phone.Name.ToLower().Contains(phoneName.ToLower()));
        }

        public IEnumerable<IProducer> SearchProducerByName(string producerName)
        {
            return _dao.GetAllProducers().Where(producer => producer.Name.ToLower().Contains(producerName.ToLower()));
        }
        public IEnumerable<string> GetUniqueAddresses()
        {
            return _dao.GetAllProducers().Select(producer => producer.Address).Distinct().ToList();
        }

        public IEnumerable<string> GetUniqueYearsOfProduction()
        {
            return _dao.GetAllPhones().Select(phone => phone.YearOfProduction.ToString()).Distinct().ToList();
        }
    }
}