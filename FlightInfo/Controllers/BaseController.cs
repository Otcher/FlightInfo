using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsAdmin()
        {
            byte[] isAdminValue = new byte[1000];
            HttpContext.Session.TryGetValue("IsAdmin", out isAdminValue);

            bool isAdmin = (HttpContext != null) &&
                           (HttpContext.Session != null) &&
                           (isAdminValue != null) &&
                           (System.Text.Encoding.UTF8.GetString(isAdminValue) == "true");

            ViewData["IsAdmin"] = isAdmin;

            return isAdmin;
        }
    }
}
