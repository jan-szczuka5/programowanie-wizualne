using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _148075._148159.PhonesCatalog.Web;
using _148075._148159.PhonesCatalog.Web.Models;
using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;

namespace _148075._148159.PhonesCatalog.Web.Controllers
{
    public class PhonesController : Controller
    {
        private readonly BLC.BLC _blc;

        public PhonesController()
        {
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"]!;
            _blc = BLC.BLC.GetInstance(libraryName);
        }

        // GET: Phones
        public IActionResult Index(string searchTerm, string filterByProducer, SoftwareType? filterBySoftwareType, int? filterByYear)
        {
            IEnumerable<IPhone> phones;

            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrEmpty(filterByProducer) || filterBySoftwareType.HasValue || filterByYear.HasValue)
            {
                phones = _blc.GetPhones();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    phones = phones.Intersect(_blc.SearchPhoneByName(searchTerm));
                }

                if (!string.IsNullOrEmpty(filterByProducer))
                {
                    phones = phones.Intersect(_blc.FilterPhoneByProducer(filterByProducer));
                }

                if (filterBySoftwareType.HasValue)
                {
                    phones = phones.Intersect(_blc.FilterPhoneBySoftwareType(filterBySoftwareType.Value));
                }

                if (filterByYear.HasValue)
                {
                    phones = phones.Intersect(_blc.FilterPhoneByYear(filterByYear.Value));
                }
            }
            else
            {
                phones = _blc.GetPhones();
            }

            ViewBag.ProducerFilterOptions = _blc.GetAllProducersNames();
            ViewBag.SoftwareTypeFilterOptions = Enum.GetValues(typeof(SoftwareType)).Cast<SoftwareType>();
            ViewBag.YearFilterOptions = _blc.GetUniqueYearsOfProduction();

            return View(phones);
        }

        // GET: Phones/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = _blc.GetPhoneById(id.Value);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            ViewBag.Producers = _blc.GetProducers();
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                var phone = new DAOMock1.BO.Phone();
                phone.Name = collection["Name"];
                phone.YearOfProduction = int.Parse(collection["YearOfProduction"]);
                phone.AlreadySold = int.Parse(collection["AlreadySold"]);
                phone.Price = int.Parse(collection["Price"]);
                phone.SoftwareType = (SoftwareType)Enum.Parse(typeof(SoftwareType), collection["SoftwareType"]);
                var producerObject = _blc.GetProducerById(int.Parse(collection["Producer"]));
                Console.WriteLine($"Producer object type: {producerObject.GetType().FullName}");
                phone.Producer = (Interfaces.IProducer)producerObject;

                _blc.CreatePhone(phone);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Phones/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = _blc.GetPhoneById(id.Value);
            if (phone == null)
            {
                return NotFound();
            }
            ViewBag.Producers = _blc.GetProducers();
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var phone = _blc.GetPhoneById(id);
                phone.Name = collection["Name"];
                phone.YearOfProduction = int.Parse(collection["YearOfProduction"]);
                phone.AlreadySold = int.Parse(collection["AlreadySold"]);
                phone.Price = int.Parse(collection["Price"]);
                phone.SoftwareType = (SoftwareType)Enum.Parse(typeof(SoftwareType), collection["SoftwareType"]);
                phone.Producer = (Interfaces.IProducer)_blc.GetProducerById(int.Parse(collection["Producer"]));

                _blc.UpdatePhone(phone);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Phones/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = _blc.GetPhoneById(id.Value);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _blc.DeletePhone(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
