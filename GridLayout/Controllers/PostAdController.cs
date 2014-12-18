using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLayout.Models;

namespace GridLayout.Controllers
{
    public class PostAdController : Controller
    {

        adWordEntities1 entities;

        public PostAdController()
        {
            entities = new adWordEntities1();
        }

        //
        // GET: /PostAd/
        public ActionResult Index()
        {
            IQueryable<AdData> adDetails = entities.AdDatas;

            return View(adDetails);
        }

        //
        // GET: /PostAd/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PostAd/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PostAd/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PostAd/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PostAd/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PostAd/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PostAd/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
