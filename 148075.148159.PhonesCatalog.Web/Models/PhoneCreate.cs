using _148075._148159.PhonesCatalog.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace _148075._148159.PhonesCatalog.Web.Models
{
    public class PhoneCreate
    {
        public string Name { get; set; }
        [Display(Name = "Year of production")]
        [Range(0, 2024)]
        public int YearOfProduction { get; set; }
        public SoftwareType SoftwareType { get; set; }
        public int ProducerID { get; set; }
        public int AlreadySold { get; set; }
        public int Price { get; set; }
        public List<SelectListItem> Producers { get; set; } = new List<SelectListItem>();
    }
}
