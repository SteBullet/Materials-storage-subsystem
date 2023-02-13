using Materials_storage_subsystem.Data;
using Materials_storage_subsystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult WarehousesListPage()
        {
            var warehouses = _context.Warehouses.ToList();
            return View(warehouses);
        }

        [HttpGet]
        public IActionResult WarehouseDetailsPage(int id)
        {
            var warehouse = _context.Warehouses.First(m => m.Id == id);
            return View(warehouse);
        }

        [HttpGet]
        public IActionResult WarehouseEditPage(int id)
        {
            var warehouse = _context.Warehouses.First(m => m.Id == id);
            return View(warehouse);
        }

        [HttpPost]
        public IActionResult WarehouseEditPage(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
            _context.SaveChanges();
            return View("WarehousesListPage", _context.Warehouses.ToList());
        }

        [HttpGet]
        public IActionResult WarehouseCreate()
        {
            var model = new Warehouse();
            return View(model);
        }

        [HttpPost]
        public IActionResult WarehouseCreate(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();
            return View("WarehousesListPage", _context.Warehouses.ToList());
        }

        [HttpGet]
        public IActionResult WarehouseDelete(int id)
        {
            var warehouse = _context.Warehouses.First(m => m.Id == id);
            _context.Warehouses.Remove(warehouse);
            _context.SaveChanges();
            return View("WarehousesListPage", _context.Warehouses.ToList());
        }

        [HttpGet]
        public IActionResult WarehouseRemainings(int id)
        {
            var remainings = _context.MaterialRemainings.Where(x => x.WarehouseId == id).ToList();
            foreach (var item in remainings)
            {
                item.Material = _context.Materials.First(x => x.Id == item.MaterialId);
            }
            return View(remainings);
        }

        [HttpGet]
        public IActionResult MoveRequestsList()
        {
            var moveRequests = _context.MoveRequests.Include(x => x.Material).ToList();
            return View(moveRequests);
        }

        [HttpGet]
        public IActionResult MoveRequestCreate()
        {
            ViewBag.WarehousesId = new SelectList(_context.Warehouses.Select(w => w.Id).ToList());
            ViewBag.Materials = new SelectList(_context.Materials.ToList());
            return View(new MoveRequest());
        }

        [HttpPost]
        public IActionResult MoveRequestCreate(MoveRequest Model)
        {
            _context.MoveRequests.Add(Model);
            _context.SaveChanges();
            return View("MoveRequestsList", _context.MoveRequests.Include(x => x.Material).ToList());
        }

        [HttpGet]
        public IActionResult MoveRequestDelete(int id)
        {
            _context.MoveRequests.Remove(_context.MoveRequests.First(m => m.Id == id));
            _context.SaveChanges();
            return View("MoveRequestsList", _context.MoveRequests.Include(x => x.Material).ToList());
        }

        [HttpGet]
        public IActionResult MoveRequestEdit(int id)
        {
            ViewBag.WarehousesId = new SelectList(_context.Warehouses.Select(w => w.Id).ToList());
            ViewBag.Materials = new SelectList(_context.Materials.ToList());
            return View(new MoveRequest());
        }

        [HttpPost]
        public IActionResult MoveRequestEdit(MoveRequest Model)
        {
            _context.MoveRequests.Update(Model);
            _context.SaveChanges();
            return View("MoveRequestsList", _context.MoveRequests.Include(x => x.Material).ToList());
        }
    }
}
