using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GridLayout.Models;
using System.IO;

namespace GridLayout.Controllers
{
    public class AdDataController : Controller
    {
        private adWordEntities db = new adWordEntities();
        
        //public ApplicationUser getUser()
        //{
        //    ApplicationUser user;
        //    return user;
        //}

        // GET: /AdData/
        public ActionResult Index()
        {
            var addatas = db.AdDatas.Include(a => a.AdType).Include(a => a.AspNetUser).Include(a => a.Category).Include(a => a.City);
            return View(addatas.ToList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile(string Title)
        {
            int Id = 1;

            if (Id > 0)
            {
                if (Request.Files["file"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                    string path1 = string.Format("{0}/{1}{2}", Server.MapPath("~/Images/Ad"), Id, extension);
                    if (System.IO.File.Exists(path1))
                        System.IO.File.Delete(path1);

                    Request.Files["file"].SaveAs(path1);

                }
                ViewData["Success"] = "Success";
            }
            else
            {
                ViewData["Success"] = "Upload Failed";
            }
            return View();
        }

        public string Upload(HttpPostedFileBase FileData)
        {
            /* 
            * 
            * Do something with the FileData 
            * 
            */
            return "Upload OK!";
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
            //ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName");
            ViewBag.Category_Id = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.City_Id = new SelectList(db.Cities, "ID", "CityName");

            var user = new ApplicationUser() { UserName = User.Identity.Name };
            //addata.User_Id = user.UserName;

            return View();
        }

        // POST: /AdData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,User_Id,City_Id,Category_Id,AdType_Id,Ad_Title,Ad_Description,Ad_Photo,Mobile")] AdData addata)
        {

            var user = User.Identity.Name;

            if (ModelState.IsValid)
            {

                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                string filename = Path.GetFileName(Request.Files[0].FileName);
                Request.Files[0].SaveAs(Path.Combine(path, filename));
                string imgSrc = "/uploads/" + filename;
                addata.Ad_Photo = imgSrc;

                db.AdDatas.Add(addata);
                db.SaveChanges();


                int Id = (from a in db.AdDatas
                select a.Id).Max();


                //if (Request.Files["file"].ContentLength > 0)
                //{
                //    string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                //    string path1 = string.Format("{0}/{1}{2}", Server.MapPath("~/Images/Ad"), Id, extension);
                //    if (System.IO.File.Exists(path1))
                //        System.IO.File.Delete(path1);

                //    Request.Files["file"].SaveAs(path1);

                //}


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
            //ViewBag.User_Id = new SelectList(db.AspNetUsers, "Id", "UserName", addata.User_Id);
            ViewBag.Category_Id = new SelectList(db.Categories, "ID", "CategoryName", addata.Category_Id);
            ViewBag.City_Id = new SelectList(db.Cities, "ID", "CityName", addata.City_Id);
            
            var user = new ApplicationUser() { UserName = User.Identity.Name };
            addata.UserName = user.UserName;
            
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
