using Materials_storage_subsystem.Data;
using Materials_storage_subsystem.Models;
using Materials_storage_subsystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Materials_storage_subsystem.Controllers
{
    public class WarehouseManagerController : Controller
    {
        private readonly ILogger<WarehouseManagerController> _logger;
        private readonly ApplicationContext _context;

        public WarehouseManagerController(ILogger<WarehouseManagerController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult ExpenseSheetList()
        {
            int userId = Int32.Parse(Request.Cookies["userId"]);
            var expenseSheets = _context.ExpenseSheets.Where(es => es.WarehouseId == _context.Users.First(u => u.Id == userId).WarehouseId).ToList();
            return View(expenseSheets);
        }

        [HttpGet]
        public IActionResult ExpenseSheetPage(int id)
        {
            var expenseSheet = _context.ExpenseSheets.Include(e => e.Warehouse).First(e => e.Id == id);
            return View(expenseSheet);
        }

        [HttpGet]
        public IActionResult ExpenseSheetEdit(int id)
        {
            ExpenseSheetEditModel Model = new ExpenseSheetEditModel();
            Model.ExpenseSheet = _context.ExpenseSheets.Include(x => x.Expenses).ThenInclude(x => x.Material).First(x => x.Id == id);
            Model.Materials = _context.Materials.ToList();
            return View(Model);
        }

        [HttpGet]
        public IActionResult ExpenseSheetEditError(int id)
        {
            ExpenseSheetEditModel Model = new ExpenseSheetEditModel();
            Model.ExpenseSheet = _context.ExpenseSheets.Include(x => x.Expenses).ThenInclude(x => x.Material).First(x => x.Id == id);
            Model.Materials = _context.Materials.ToList();
            return View(Model);
        }

        [HttpPost]
        public IActionResult AddExpense(ExpenseSheetEditModel Model)
        {
            Model.MaterialMovement.ExpenseSheet = _context.ExpenseSheets.First(x => x.Id == Model.MaterialMovement.ExpenseSheetId);
            Model.MaterialMovement.Material = _context.Materials.First(x => x.Id == Model.MaterialMovement.MaterialId);
            MaterialRemaining materialRemaining = _context.MaterialRemainings.FirstOrDefault(x => x.WarehouseId == Model.MaterialMovement.ExpenseSheet.WarehouseId && x.MaterialId == Model.MaterialMovement.MaterialId);
            if (materialRemaining == null && Model.MaterialMovement.Quantity > 0)
            {
                materialRemaining = new MaterialRemaining
                {
                    Material = Model.MaterialMovement.Material,
                    MaterialId = Model.MaterialMovement.MaterialId,
                    Quantity = Model.MaterialMovement.Quantity,
                    Warehouse = Model.MaterialMovement.ExpenseSheet.Warehouse,
                    WarehouseId = Model.MaterialMovement.ExpenseSheet.WarehouseId
                };
                _context.MaterialRemainings.Add(materialRemaining);
            }
            else
                if (materialRemaining != null && materialRemaining.Quantity + Model.MaterialMovement.Quantity >= 0)
            {
                materialRemaining.Quantity += Model.MaterialMovement.Quantity;
                _context.MaterialRemainings.Update(materialRemaining);
            }
            else
            {
                return Redirect($"/WarehouseManager/ExpenseSheetEditError/{Model.MaterialMovement.ExpenseSheetId}");
            }
            _context.MaterialMovements.Add(Model.MaterialMovement);
            _context.SaveChanges();
            return Redirect($"/WarehouseManager/ExpenseSheetEdit/{Model.MaterialMovement.ExpenseSheetId}");
        }

        [HttpGet]
        public IActionResult DeleteExpense(int id)
        {
            var materialMovement = _context.MaterialMovements.First(m => m.Id == id);
            _context.MaterialMovements.Remove(materialMovement);
            _context.SaveChanges();
            return Redirect($"/WarehouseManager/ExpenseSheetEdit/{materialMovement.ExpenseSheetId}");
        }

        [HttpPost]
        public IActionResult ExpenseSheetEdit(ExpenseSheet expenseSheet)
        {
            _context.ExpenseSheets.Update(expenseSheet);
            _context.SaveChanges();
            return View("ExpenseSheetList", _context.ExpenseSheets.ToList());
        }

        [HttpGet]
        public IActionResult ExpenseSheetCreate()
        {
            int userId = Int32.Parse(Request.Cookies["userId"]);
            var Model = new ExpenseSheet() { WarehouseId = (int)_context.Users.First(u => u.Id == userId).WarehouseId };
            return View(Model);
        }

        [HttpPost]
        public IActionResult ExpenseSheetCreate(ExpenseSheet expenseSheet)
        {
            _context.ExpenseSheets.Add(expenseSheet);
            _context.SaveChanges();
            return Redirect("/WarehouseManager/ExpenseSheetList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult MaterialsCatalogPage()
        {
            var materials = _context.Materials.ToList();
            return View(materials);
        }

        [HttpGet]
        public IActionResult WarehouseDetailsPage(int id)
        {
            int userId = Int32.Parse(Request.Cookies["userId"]);
            var warehouse = _context.Warehouses.First(m => m.Id == _context.Users.First(u => u.Id == userId).WarehouseId);
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
            return View("WarehouseDetailsPage", warehouse);
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
    }
}
