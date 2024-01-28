using _148075._148159.PhonesCatalog.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace _148075._148159.PhonesCatalog.Web.Models
{
    public class PhoneDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Display(Name = "Year of production")]
        public int YearOfProduction { get; set; }
        public SoftwareType SoftwareType { get; set; }
        public string Producer { get; set; }
        public int AlreadySold { get; set; }
        public int Price { get; set; }
    }
}
