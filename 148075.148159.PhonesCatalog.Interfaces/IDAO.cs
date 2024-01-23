using _148075._148159.PhonesCatalog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<IPhone> GetAllPhones();
        IProducer CreateNewProducer(int id, string name, string address);
        IPhone CreateNewPhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType);

        void DeleteProducer(int producerId);

        void DeletePhone(int phoneId);

        void UpdateProducer(int id, string name, string address);
        void UpdatePhone(int id, string name, int producerID, int yearOfProduction, int alreadySold, int price, SoftwareType softwareType);

        IPhone? GetPhone(int phoneId);

        IProducer? GetProducer(int producerId);
    }
}