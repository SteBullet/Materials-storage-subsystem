using Materials_storage_subsystem.Data;
using Materials_storage_subsystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly ApplicationContext _context;

        public ManagerController(ILogger<ManagerController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MaterialDetailsPage(int id)
        {
            var material = _context.Materials.First(m => m.Id == id);
            return View(material);
        }

        [HttpGet]
        public IActionResult MaterialCreatePage()
        {
            var model = new Material();
            return View(model);
        }

        [HttpPost]
        public IActionResult MaterialCreatePage(Material material)
        {
            _context.Materials.Add(material);
            _context.SaveChanges();
            return View("MaterialsCatalogPage", _context.Materials.ToList());
        }

        [HttpGet]
        public IActionResult MaterialsCatalogPage()
        {
            var materials = _context.Materials.ToList();
            return View(materials);
        }

        [HttpGet]
        public IActionResult MaterialDelete(int id)
        {
            var material = _context.Materials.First(m => m.Id == id);
            _context.Materials.Remove(material);
            _context.SaveChanges();
            return View("MaterialsCatalogPage", _context.Materials.ToList());
        }

        [HttpGet]
        public IActionResult MaterialEditPage(int id)
        {
            var material = _context.Materials.First(m => m.Id == id);
            return View(material);
        }

        [HttpPost]
        public IActionResult MaterialEditPage(Material material)
        {
            _context.Materials.Update(material);
            _context.SaveChanges();
            return View("MaterialsCatalogPage", _context.Materials.ToList());
        }
    }
}
