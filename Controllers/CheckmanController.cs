﻿using Materials_storage_subsystem.Data;
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
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public CheckmanController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        /*public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }*/

        [HttpGet]
        public IActionResult ExpenseSheetList()
        {
            int id = Int32.Parse(Request.Cookies["userId"]);
            var expenseSheets = _context.ExpenseSheets.ToList();
            return View(expenseSheets);
        }

        [HttpGet]
        public IActionResult ExpenseSheetPage(int id)
        {
            var expenseSheet = _context.ExpenseSheets.Include(e => e.Warehouse).First(e => e.Id == id);
            return View(expenseSheet);
        }

        [HttpGet]
        public IActionResult ExpenseSheetCreate()
        {
            var WarehousesId = _context.Warehouses.Select(w => w.Id).ToList();
            var Model = new ExpenseSheetModel() { WarehousesId = WarehousesId };
            return View(Model);
        }

        [HttpPost]
        public IActionResult ExpenseSheetCreate(ExpenseSheet expenseSheet)
        {
            _context.ExpenseSheets.Add(expenseSheet);
            _context.SaveChanges();
            return Redirect("/Checkman/ExpenseSheetList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}