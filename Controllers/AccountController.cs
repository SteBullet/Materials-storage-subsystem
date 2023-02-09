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
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Login([Bind("Login,Password")] User user)
        {
            if (_context.Checkmen.Any(a => a.Password == user.Password && a.Login == user.Login))
            {
                long id = _context.Checkmen.Where(a => a.Password == user.Password && a.Login == user.Login).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());

                return RedirectToAction("ExpenseSheetList", "Checkman", new { id = id });
            }
            else if (_context.Managers.Any(a => a.Password == user.Password && a.Login == user.Login))
            {
                long id = _context.Managers.Where(a => a.Password == user.Password && a.Login == user.Login).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());

                return RedirectToAction("MaterialsCatalogPage", "Manager");
            }
            else if (_context.Admins.Any(a => a.Password == user.Password && a.Login == user.Login))
            {
                long id = _context.Admins.Where(a => a.Password == user.Password && a.Login == user.Login).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());

                return RedirectToAction("UsersList", "Admin");
            }
            else if (_context.WarehouseManagers.Any(a => a.Password == user.Password && a.Login == user.Login))
            {
                long id = _context.WarehouseManagers.Where(a => a.Password == user.Password && a.Login == user.Login).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());

                return RedirectToAction("ExpenseSheetList", "WarehouseManager");
            }
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("userId");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            var Disc = new List<String>() { "Accountant", "SuperVisor", "Admin" };
            ViewBag.Discriminators = new SelectList(Disc);
            IEnumerable<User> users = _context.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "Name", "Surname");
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Redirect("/Account/GetUsers");
        }

        public IActionResult GetUsers()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult EditUser(User d)
        {

            _context.Entry(d).State = EntityState.Modified;
            _context.SaveChanges();
            return Redirect("/Account/GetUsers");
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            return View(_context.Users.Where(x => x.Id == id).FirstOrDefault());

        }

        [HttpGet]
        public IActionResult InfoUser(int id)
        {
            return View(_context.Users.Where(x => x.Id == id).FirstOrDefault());
        }

        public IActionResult DeleteUser(int id, User user)
        {
            user = _context.Users.Where(d => d.Id == id).First();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Redirect("/Account/GetUsers");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
