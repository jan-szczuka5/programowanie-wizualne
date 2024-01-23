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
        IProducer CreateNewProducer(IProducer producer);
        IPhone CreateNewPhone(IPhone phone);
        void CreatePhone(IPhone phone);

        IPhone NewPhone();

        void DeleteProducer(int producerId);

        void DeletePhone(int phoneId);

        void UpdateProducer(IProducer producer);
        void UpdatePhone(IPhone phone);
    }
}