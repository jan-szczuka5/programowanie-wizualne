using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;

namespace _148075._148159.PhonesCatalog.Web.Models
{

    public class Phone
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public int YearOfProduction { get; set; }
        public int AlreadySold { get; set; }
        public int Price { get; set; }
        public SoftwareType SoftwareType { get; set; }
    }
}
