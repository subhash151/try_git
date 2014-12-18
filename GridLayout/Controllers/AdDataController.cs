using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GridLayout.Models;

namespace GridLayout.Controllers
{
    public class AdDataController : Controller
    {
        private adWordEntities db = new adWordEntities();

        // GET: /AdData/
        public ActionResult Index()
        {
            var addatas = db.AdDatas.Include(a => a.AdType).Include(a => a.AspNetUser).Include(a => a.Category).Include(a => a.City);
            return View(addatas.ToList());
        }

        // GET: /AdData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdData addata = db.AdDatas.Find(id);
            if (addata == null)
            {
                return HttpNotFound();
            }
            return View(addata);
        }

        // GET: /AdData/Create
        public ActionResult Create()
        {
            ViewBag.AdType_Id = new SelectList(db.AdTypes, "ID", "AdTypeName");
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName");
            ViewBag.Category_Id = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.City_Id = new SelectList(db.Cities, "ID", "CityName");
            return View();
        }

        // POST: /AdData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,User_Id,City_Id,Locality_Id,Category_Id,AdType_Id,Ad_Title,Ad_Description,Ad_Photo,Mobile")] AdData addata)
        {
            if (ModelState.IsValid)
            {
                db.AdDatas.Add(addata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdType_Id = new SelectList(db.AdTypes, "ID", "AdTypeName", addata.AdType_Id);
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName", addata.User_Id);
            ViewBag.Category_Id = new SelectList(db.Categories, "ID", "CategoryName", addata.Category_Id);
            ViewBag.City_Id = new SelectList(db.Cities, "ID", "CityName", addata.City_Id);
            return View(addata);
        }

        // GET: /AdData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdData addata = db.AdDatas.Find(id);
            if (addata == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdType_Id = new SelectList(db.AdTypes, "ID", "AdTypeName", addata.AdType_Id);
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName", addata.User_Id);
            ViewBag.Category_Id = new SelectList(db.Categories, "ID", "CategoryName", addata.Category_Id);
            ViewBag.City_Id = new SelectList(db.Cities, "ID", "CityName", addata.City_Id);
            return View(addata);
        }

        // POST: /AdData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,User_Id,City_Id,Locality_Id,Category_Id,AdType_Id,Ad_Title,Ad_Description,Ad_Photo,Mobile")] AdData addata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdType_Id = new SelectList(db.AdTypes, "ID", "AdTypeName", addata.AdType_Id);
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName", addata.User_Id);
            ViewBag.Category_Id = new SelectList(db.Categories, "ID", "CategoryName", addata.Category_Id);
            ViewBag.City_Id = new SelectList(db.Cities, "ID", "CityName", addata.City_Id);
            return View(addata);
        }

        // GET: /AdData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdData addata = db.AdDatas.Find(id);
            if (addata == null)
            {
                return HttpNotFound();
            }
            return View(addata);
        }

        // POST: /AdData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdData addata = db.AdDatas.Find(id);
            db.AdDatas.Remove(addata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
