using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLayout.Models;

namespace GridLayout.Controllers
{
    public class HomeController : Controller
    {
        private adWordEntities entities;

        public HomeController()
        {
            entities = new adWordEntities();
        }

        public ActionResult Index()
        {
            var query = from City in entities.Cities
                        select City;

            return View(entities);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}