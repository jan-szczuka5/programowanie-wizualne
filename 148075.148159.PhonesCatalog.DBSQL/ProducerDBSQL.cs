using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _148075._148159.PhonesCatalog.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace _148075._148159.PhonesCatalog.DBSQL
{
    public class ProducerDBSQL
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public IProducer ToIProducer()
        {
            return new Producer() { ID = ID, Name = Name, Address = Address };
        }

        public class Producer : IProducer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
        }
    }
}