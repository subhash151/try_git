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

        public HomeController()
        {
            entities = new adWordEntities();
        }

        public ActionResult Index(int? cityId, int? catId, string search=null)
        {
            IQueryable<AdData> adDetails = entities.AdDatas;
            return View(adDetails.ToList());
        }

        public ActionResult fetchData(int? cityId, int? catId, string search = null)
        {
            IQueryable<AdData> adDetails = null;
            //if (cityId != null || catId != null || search != null)
            //{
            //    adDetails = entities.AdDatas;
            //}
            //else
            //{
            //    var query = "";
            //    if (cityId != null)
            //    {
            //        query = query + "adData.City_Id ==" + cityId + "&&";
            //    }
            //    else if (catId != null)
            //    {
            //        query = query + "adData.Category_Id ==" + cityId + "&&";
            //    }
            //    else if (search != null)
            //    {
            //        query = query + "adData.Ad_Descriptio.Contains(" + search + ")";
            //    }
            //}

            adDetails = from adData in entities.AdDatas
                        where (cityId != null ? adData.City_Id == cityId : (catId != null ? adData.Category_Id == catId : adData.Ad_Description.Contains(search)))
                        select adData;  

            return PartialView("../Home/_LoadDataPartial", adDetails.ToList());
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