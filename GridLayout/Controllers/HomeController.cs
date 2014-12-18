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
        private adWordEntities1 entities;

        public HomeController()
        {
            entities = new adWordEntities1();
        }

        public ActionResult Index()
        {
            IQueryable<City> cities = entities.Cities;
            //var query = from City in entities.Cities
            //            select City;

            return View(cities.ToList());
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