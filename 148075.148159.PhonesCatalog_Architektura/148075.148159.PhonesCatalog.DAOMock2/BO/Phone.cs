using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.DAOMock2.BO
{
    public class Phone : IPhone
    {
        public int ID { get; set ; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public int YeadOfProduction { get; set; }
        public int AlreadySold { get; set; }
        public int Price { get; set; }
        public SoftwareType SoftwareType { get; set; }
    }
}
