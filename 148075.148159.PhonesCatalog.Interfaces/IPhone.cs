using _148075._148159.PhonesCatalog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.Interfaces
{
    public interface IPhone
    {
        int ID { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        int YearOfProduction { get; set; }
        int AlreadySold { get; set; }
        int Price { get; set; }
        SoftwareType SoftwareType { get; set; }
    }
}