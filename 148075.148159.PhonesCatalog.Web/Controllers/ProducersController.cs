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
    public class ProducersController : Controller
    {
        private readonly BLC.BLC _blc;

        public ProducersController()
        {
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"]!;
            _blc = BLC.BLC.GetInstance(libraryName);

        }

        // GET: Producers
        public IActionResult Index(string searchTerm, string filterByAddress)
        {
            var producers = _blc.GetProducers().ToList();
            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(filterByAddress))
                {
                var searchResults = _blc.SearchProducerByName(searchTerm).ToList();
                var filterResults= _blc.FilterProducerByAddress(filterByAddress).ToList();

                producers = searchResults.Intersect(filterResults).ToList();
            }
            else if (!string.IsNullOrEmpty(searchTerm))
            {
                // Perform search based on the name
                producers = _blc.SearchProducerByName(searchTerm).ToList();
            }
            else if (!string.IsNullOrEmpty(filterByAddress))
            {
                // Perform filtering based on the address
                producers = _blc.FilterProducerByAddress(filterByAddress).ToList();
            }
            else if (string.IsNullOrEmpty(filterByAddress) && string.IsNullOrEmpty(searchTerm))
            {
                // Get all producers
                producers = _blc.GetProducers().ToList();
            }
            else 
            {
                producers = new List<IProducer>();
            }
            ViewBag.UniqueAddresses = _blc.GetUniqueAddresses(); // Add this line

            return View(producers);
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducerById(id.Value);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                var producer = new DAOMock1.BO.Producer();
                producer.Name = collection["Name"];
                producer.Address = collection["Address"];
                _blc.CreateProducer(producer);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducerById(id.Value);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var producer = _blc.GetProducerById(id);
                producer.Name = collection["Name"];
                producer.Address = collection["Address"];
                _blc.UpdateProducer(producer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducerById(id.Value);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _blc.DeleteProducer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
