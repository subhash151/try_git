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
    public class LocalityController : Controller
    {
        private adWordEntities db = new adWordEntities();

        // GET: /Locality/
        public ActionResult Index()
        {
            var localities = db.Localities.Include(l => l.City);
            return View(localities.ToList());
        }

        // GET: /Locality/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locality locality = db.Localities.Find(id);
            if (locality == null)
            {
                return HttpNotFound();
            }
            return View(locality);
        }

        // GET: /Locality/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName");
            return View();
        }

        // POST: /Locality/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,LocalityName,CityID")] Locality locality)
        {
            if (ModelState.IsValid)
            {
                db.Localities.Add(locality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", locality.CityID);
            return View(locality);
        }

        // GET: /Locality/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locality locality = db.Localities.Find(id);
            if (locality == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", locality.CityID);
            return View(locality);
        }

        // POST: /Locality/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,LocalityName,CityID")] Locality locality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityName", locality.CityID);
            return View(locality);
        }

        // GET: /Locality/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locality locality = db.Localities.Find(id);
            if (locality == null)
            {
                return HttpNotFound();
            }
            return View(locality);
        }

        // POST: /Locality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locality locality = db.Localities.Find(id);
            db.Localities.Remove(locality);
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
