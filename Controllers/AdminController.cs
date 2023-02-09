using Materials_storage_subsystem.Models;
using Materials_storage_subsystem.Models.Roles;
using Materials_storage_subsystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Materials_storage_subsystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;

        public AdminController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            var roles = new List<String>() { "Admin", "Manager", "WarehouseManager", "Checkman" };
            ViewBag.Roles = new SelectList(roles);
            var warehousesId = _context.Warehouses.Select(w => w.Id).ToList();
            ViewBag.WarehousesId = new SelectList(warehousesId);
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (user.Discriminator == "Admin" || user.Discriminator == "Manager")
                user.WarehouseId = null;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Redirect("/Admin/UsersList");
        }

        public IActionResult UsersList()
        {
            return View(_context.Users.ToList());
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (user.Discriminator == "Admin" || user.Discriminator == "Manager")
                user.WarehouseId = null;
            //_context.Users.First(u => u.Id == user.Id).Discriminator = user.Discriminator;
            _context.Users.Update(user);
            //_context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Redirect("/Admin/UsersList");
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var roles = new List<String>() { "Admin", "Manager", "WarehouseManager", "Checkman" };
            ViewBag.Roles = new SelectList(roles);
            var warehousesId = _context.Warehouses.Select(w => w.Id).ToList();
            ViewBag.WarehousesId = new SelectList(warehousesId);
            return View(_context.Users.First(x => x.Id == id));
        }

        [HttpGet]
        public IActionResult UserDetails(int id)
        {
            return View(_context.Users.First(x => x.Id == id));
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.First(d => d.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Redirect("/Admin/UsersList");
        }
    }
}
