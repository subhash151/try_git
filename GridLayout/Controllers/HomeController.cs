using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GridLayout.Models;
//using System.Web.Script.Serialization;


namespace GridLayout.Controllers
{
    public class HomeController : Controller
    {
        private adWordEntities entities;

        private adWordEntities db = new adWordEntities();
        //private DbSet<AdData> query;

        public HomeController()
        {
            entities = new adWordEntities();
        }

        public ActionResult Index()
        {
            IQueryable<AdData> adDetails = entities.AdDatas;

            ViewData["TotalRecords"] = adDetails.Count();
            ViewData["RecordPerPage"] = 10;
            ViewData["CurrentPage"] = 1;
            ViewData["DisplayPage"] = Math.Ceiling((decimal)adDetails.Count() / 10);

            return View(adDetails.Take(10).ToList());
        }

        //public ActionResult fetchTopThree(int? cityId, int? catId, int currentPage, int recordPerPage, string search = null)
        //{
        //    IQueryable<AdData> adDetails = entities.AdDatas;

        //    if (!((cityId == null || cityId == 0) && (catId == null || catId == 0) && (search == null || search == string.Empty)))
        //    {
        //        if (!(cityId == null || cityId == 0))
        //        {
        //            adDetails = adDetails.Where(cty => cty.City_Id == (int)cityId);
        //        }

        //        if (!(catId == null || catId == 0))
        //        {
        //            adDetails = adDetails.Where(cat => cat.Category_Id == (int)catId);
        //        }

        //        if (!(search == null || search == string.Empty))
        //        {
        //            adDetails = adDetails.Where(srch => srch.Ad_Description.Contains(search));
        //        }

        //        adDetails = adDetails.OrderBy(ord => ord.Ad_Photo);
        //    }

        //    adDetails = adDetails.OrderBy(ord => ord.Ad_Photo);
        //    return PartialView("../Home/_LoadTopThreePartial", adDetails.Take(3).ToList());
        //}

        public ActionResult fetchData(int? cityId, int? catId, int currentPage, int recordPerPage, string search = null)
        {
            IQueryable<AdData> adDetails = entities.AdDatas;

            if (!((cityId == null || cityId == 0) && (catId == null || catId == 0) && (search == null || search == string.Empty)))
            {
                if(!(cityId == null || cityId == 0))
                {
                    adDetails = adDetails.Where(cty => cty.City_Id == (int)cityId);
                }
                
                if (!(catId == null || catId == 0))
                {
                    adDetails = adDetails.Where(cat => cat.Category_Id == (int)catId);
                }
                
                if (!(search == null || search == string.Empty))
                {
                    adDetails = adDetails.Where(srch => srch.Ad_Description.Contains(search));
                }
            }

            ViewData["TotalRecords"] = adDetails.Count();
            ViewData["RecordPerPage"] = recordPerPage;
            ViewData["CurrentPage"] = currentPage;
            ViewData["DisplayPage"] = Math.Ceiling((decimal)adDetails.Count() / recordPerPage);
           
            adDetails = adDetails.OrderBy(ord => ord.Ad_Photo);
            //adDetails.Skip((currentPage - 1) * recordPerPage).Take(recordPerPage);
            return PartialView("../Home/_LoadDataPartial", adDetails.Skip((currentPage - 1) * recordPerPage).Take(recordPerPage).ToList());
        }

        public JsonResult GetCity()
        {
            List<City> cities = new List<City>();
            foreach (var cit in entities.Cities)
            {
                City cty = new City();
                cty.ID = cit.ID;
                cty.CityName = cit.CityName;
                cities.Add(cty);
            }

            JsonResult rslt = Json(cities, JsonRequestBehavior.AllowGet);
            return rslt;
        }

        public JsonResult GetCategory()
        {
            List<Category> categories = new List<Category>();
            foreach (var cat in entities.Categories)
            {
                Category category = new Category();
                category.ID = cat.ID;
                category.CategoryName = cat.CategoryName;
                categories.Add(category);
            }

            JsonResult rslt = Json(categories, JsonRequestBehavior.AllowGet);
            return rslt;
        }

        public JsonResult GetAdData()
        {
            List<AdData> addta = new List<AdData>();
            foreach (var ad in entities.AdDatas)
            {
                AdData dta = new AdData();
                dta.Ad_Title = ad.Ad_Title;
                dta.Ad_Photo = ad.Ad_Photo;
                dta.Ad_Description = ad.Ad_Description;
                addta.Add(dta);
            }

            JsonResult rslt = Json(addta, JsonRequestBehavior.AllowGet);
            return rslt;
        }

        public JsonResult GetTopThree()
        {
            //List<AdData> adDta = new List<AdData>();
            //foreach (var dta in entities.AdDatas)
            //{
            //    AdData ad = new AdData();
            //    ad.Ad_Photo = dta.Ad_Photo;
            //    ad.Ad_Title = dta.Ad_Title;
            //    ad.Ad_Description = dta.Ad_Description;
            //    adDta.Add(ad);
            //}

            var data = entities.AdDatas.ToList();

            var collection = data.Select(x => new
            {
                x.Ad_Title,
                x.Ad_Photo,
                x.Ad_Description,
            });

            JsonResult rslt = Json(collection, JsonRequestBehavior.AllowGet);
            return rslt;
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