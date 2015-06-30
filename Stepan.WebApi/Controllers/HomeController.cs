using Stepan.Repository;
using Stepan.Repository.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stepan.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Test()
        {
            ViewBag.Title = "Home Page";

            return View();
        }  
    }
}
