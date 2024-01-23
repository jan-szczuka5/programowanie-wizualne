using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _148075._148159.PhonesCatalog.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace _148075._148159.PhonesCatalog.DBSQL.BO
{
    public class Producer : IProducer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<IPhone> Phones { get; set; }
    }
}
