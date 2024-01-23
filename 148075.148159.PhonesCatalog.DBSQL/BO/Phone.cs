using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _148075._148159.PhonesCatalog.Interfaces;
using System.ComponentModel.DataAnnotations;
using _148075._148159.PhonesCatalog.Core;

namespace _148075._148159.PhonesCatalog.DBSQL.BO
{
    public class Phone : IPhone
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public int YearOfProduction { get; set; }
        public int AlreadySold { get; set; }
        public int Price { get; set; }
        public SoftwareType SoftwareType { get; set; }
    }

}