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
    public class AdTypeController : Controller
    {
        private adWordEntities db = new adWordEntities();

        // GET: /AdType/
        public ActionResult Index()
        {
            return View(db.AdTypes.ToList());
        }

        // GET: /AdType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdType adtype = db.AdTypes.Find(id);
            if (adtype == null)
            {
                return HttpNotFound();
            }
            return View(adtype);
        }

        // GET: /AdType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AdType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,AdTypeName")] AdType adtype)
        {
            if (ModelState.IsValid)
            {
                db.AdTypes.Add(adtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adtype);
        }

        // GET: /AdType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdType adtype = db.AdTypes.Find(id);
            if (adtype == null)
            {
                return HttpNotFound();
            }
            return View(adtype);
        }

        // POST: /AdType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,AdTypeName")] AdType adtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adtype);
        }

        // GET: /AdType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdType adtype = db.AdTypes.Find(id);
            if (adtype == null)
            {
                return HttpNotFound();
            }
            return View(adtype);
        }

        // POST: /AdType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdType adtype = db.AdTypes.Find(id);
            db.AdTypes.Remove(adtype);
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
