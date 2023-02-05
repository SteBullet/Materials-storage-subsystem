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
    public class CheckmanController : Controller
    {
        private readonly ILogger<CheckmanController> _logger;
        private readonly ApplicationContext _context;

        public CheckmanController(ILogger<CheckmanController> logger, ApplicationContext context)
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
                return Redirect($"/Checkman/ExpenseSheetEditError/{Model.MaterialMovement.ExpenseSheetId}");
            }
            _context.MaterialMovements.Add(Model.MaterialMovement);
            _context.SaveChanges();
            return Redirect($"/Checkman/ExpenseSheetEdit/{Model.MaterialMovement.ExpenseSheetId}");
        }

        [HttpGet]
        public IActionResult DeleteExpense(int id)
        {
            var materialMovement = _context.MaterialMovements.First(m => m.Id == id);
            _context.MaterialMovements.Remove(materialMovement);
            _context.SaveChanges();
            return Redirect($"/Checkman/ExpenseSheetEdit/{materialMovement.ExpenseSheetId}");
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
            var Model = new ExpenseSheetCreateModel() { WarehousesId = _context.Users.First(u => u.Id == userId).WarehouseId };
            return View(Model);
        }

        [HttpPost]
        public IActionResult ExpenseSheetCreate(ExpenseSheet expenseSheet)
        {
            _context.ExpenseSheets.Add(expenseSheet);
            _context.SaveChanges();
            return Redirect("/Checkman/ExpenseSheetList");
        }

        [HttpGet]
        public IActionResult MaterialsCatalogPage()
        {
            var materials = _context.Materials.ToList();
            return View(materials);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
