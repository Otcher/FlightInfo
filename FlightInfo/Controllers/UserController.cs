using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightInfo.Data;
using FlightInfo.Models;

namespace FlightInfo.Controllers
{
    public class UsersController : BaseController
    {
        private readonly FlightInfoContext _context;

        public UsersController(FlightInfoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(m => m.Name == username);

            if (user != null && user.Password == password)
            {
                HttpContext.Session.Set("IsAdmin", System.Text.Encoding.UTF8.GetBytes("true"));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.Set("IsAdmin", System.Text.Encoding.UTF8.GetBytes("false"));

                ViewBag.Error = "Invalid username or password";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public async Task<IActionResult> Logout(string username, string password)
        {
            HttpContext.Session.Set("IsAdmin", System.Text.Encoding.UTF8.GetBytes("false"));
            return RedirectToAction("Index", "Home");
        }
             
    }
}