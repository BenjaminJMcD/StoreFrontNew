using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using PagedList;
using PagedList.Mvc;
using StoreFrontLab.UI.MVC.Utilities;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class RecordsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Records
        public ActionResult Index(int page = 1)
        {
            int pageSize = 5;

            var records = db.Records.OrderBy(r => r.BandMusician).ToList();

            return View(records.ToPagedList(page, pageSize));

            //var records = db.Records.Include(r => r.Category).Include(r => r.Genre).Include(r => r.Producer).Include(r => r.StockStatus);
            //return View(records.ToList());

        }

        // GET: Records/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Records/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "FirstName");
            ViewBag.StockID = new SelectList(db.StockStatuses, "StockID", "Status");
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordID,RecordName,BandMusician,GenreID,ReleaseDate,Price,CoverImage,StockID,ProducerID,CategoryID,ColoredLP,Logo")] Record record, HttpPostedFileBase logo)

        {



            if (ModelState.IsValid)
            {
                #region File Upload

                string file = "NoImageAvailable.jpeg";

                if (logo != null)
                {
                    file = logo.FileName;

                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".webp" };

                    //Check that the uploaded file ext is in our list of acceptable extensions
                    //and check that the file size is <= 4MB, which is default maximum for ASP.NET

                    if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                    {
                        //Create a new filename (using a GUID)
                        file = Guid.NewGuid() + ext;
                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/RecordCovers/");

                        Image convertedImage = Image.FromStream(logo.InputStream);

                        int maxImageSize = 300;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                    }

                    //No matter what, update the photourl with the value of the file variable
                    record.CoverImage = file;
                }

                #endregion

                db.Records.Add(record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", record.CategoryID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", record.GenreID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "FirstName", record.ProducerID);
            ViewBag.StockID = new SelectList(db.StockStatuses, "StockID", "Status", record.StockID);
            return View(record);
        }

        // GET: Records/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", record.CategoryID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", record.GenreID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "FirstName", record.ProducerID);
            ViewBag.StockID = new SelectList(db.StockStatuses, "StockID", "Status", record.StockID);
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordID,RecordName,BandMusician,GenreID,ReleaseDate,Price,CoverImage,StockID,ProducerID,CategoryID,ColoredLP, Logo")] Record record, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                //Check to see if a new file has been uploaded. If not, the HiddenFor() in the view will maintain the original value.
                string file = "~/Content/RecordCovers/NoImageAvailable.jpeg";

                //If a file has been uploaded
                if (logo != null)
                {
                    //Get the name
                    file = logo.FileName;

                    //Capture the extension
                    string ext = file.Substring(file.LastIndexOf('.'));

                    //Create a "whitelist" of accepted extensions
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".webp" };

                    //Check that the uploaded file ext is in our list of acceptable extensions
                    //and that the file size is <= 4MB max

                    if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                    {
                        //Create new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/RecordCovers/");

                        Image convertedImage = Image.FromStream(logo.InputStream);

                        int maxImageSize = 300;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion

                        if (record.CoverImage != null && record.CoverImage != "NoImageAvailable.jpeg")
                        {
                            string path = Server.MapPath("~/Content/RecordCovers/");
                            ImageUtility.Delete(path, record.CoverImage);
                        }

                        //Update the property of the object
                        record.CoverImage = file;
                    }
                }

                #endregion

                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", record.CategoryID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", record.GenreID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "FirstName", record.ProducerID);
            ViewBag.StockID = new SelectList(db.StockStatuses, "StockID", "Status", record.StockID);
            return View(record);
        }

        // GET: Records/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Record record = db.Records.Find(id);

            string path = Server.MapPath("~/Content/RecordCovers/");
            ImageUtility.Delete(path, record.CoverImage);

            db.Records.Remove(record);
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
