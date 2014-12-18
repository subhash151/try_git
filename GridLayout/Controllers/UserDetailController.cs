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
    public class UserDetailController : Controller
    {
        private adWordEntities db = new adWordEntities();

        // GET: /UserDetail/
        public ActionResult Index()
        {
            var userdetails = db.UserDetails.Include(u => u.AspNetUser);
            return View(userdetails.ToList());
        }

        // GET: /UserDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userdetail = db.UserDetails.Find(id);
            if (userdetail == null)
            {
                return HttpNotFound();
            }
            return View(userdetail);
        }

        // GET: /UserDetail/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: /UserDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,User_Id,User_Name,User_Address,User_Photo,Mobile")] UserDetail userdetail)
        {
            if (ModelState.IsValid)
            {
                db.UserDetails.Add(userdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName", userdetail.User_Id);
            return View(userdetail);
        }

        // GET: /UserDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userdetail = db.UserDetails.Find(id);
            if (userdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName", userdetail.User_Id);
            return View(userdetail);
        }

        // POST: /UserDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,User_Id,User_Name,User_Address,User_Photo,Mobile")] UserDetail userdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName", userdetail.User_Id);
            return View(userdetail);
        }

        // GET: /UserDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userdetail = db.UserDetails.Find(id);
            if (userdetail == null)
            {
                return HttpNotFound();
            }
            return View(userdetail);
        }

        // POST: /UserDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDetail userdetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userdetail);
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
