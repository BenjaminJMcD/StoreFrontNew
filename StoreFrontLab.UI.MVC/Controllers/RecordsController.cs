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
using StoreFrontLab.UI.MVC.Models;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class RecordsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Records
        public ActionResult Index(string sortOrder, string searchString, int page = 1)
        {
            int pageSize = 10;

            #region Optional Search Filter

            //Check if the searchFilter string was provided/has content
            if (String.IsNullOrEmpty(searchString))
            {
                         
                var records = db.Records.Include(r => r.Category).Include(r => r.Genre).Include(r => r.Producer).Include(r => r.StockStatus).OrderBy(r => r.BandMusician).ToList();
                //return View(records.ToList());

                #region ColumnSortFailure
                
                //ViewBag.RecordNameSortParm = sortOrder == "recordname" ? "recordname_desc" : "recordname";
                //ViewBag.ArtistSortParm = sortOrder == "artist" ? "artist_desc" : "artist";
                //ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
                //ViewBag.ReleaseDateSortParm = sortOrder == "releasedate" ? "releasedate_desc" : "releasedate";

                //switch (sortOrder)
                //{
                //    case "recordname":
                //        records = records.FirstOrDefault.OrderBy(s => s.RecordName);
                //        break;
                //    case "recordname_desc":
                //        records = records.OrderByDescending(s => s.RecordName);
                //        break;
                //    case "artist":
                //        records = records.OrderBy(s => s.BandMusician);
                //        break;
                //    case "artist_desc":
                //        records = records.OrderByDescending(s => s.BandMusician);
                //        break;
                //    case "price":
                //        records = records.OrderBy(s => s.Price);
                //        break;
                //    case "price_desc":
                //        records = records.OrderByDescending(s => s.Price);
                //        break;
                //    case "releasedate":
                //        records = records.OrderBy(s => s.ReleaseDate);
                //        break;
                //    case "releasedate_desc":
                //        records = records.OrderByDescending(s => s.ReleaseDate);
                //        break;
                //    default:
                //        records = records.OrderBy(s => s.RecordID);
                //        break;
                //}
                #endregion

                return View(records.ToPagedList(page, pageSize));
            }
            else
            {
                //If the optional search IS used, then filter the results by those
                //criteria (compare to first or last name and ignore casing concerns)

                //v1 LINQ Method/Lambda Syntax
                string searchUpCase = searchString.ToUpper();

                List<Record> searchResults = db.Records.Where(
                    a => a.RecordName.ToUpper().Contains(searchUpCase) ||
                    a.BandMusician.ToUpper().Contains(searchUpCase)).ToList();

                return View(searchResults);

            }

            #endregion


        

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

        #region Custom Add-to-Cart Functionality (Called from Details View)
        [HttpPost]
        public ActionResult AddToCart(int qty, int recordID)
        {
            //Create an empty shell for the LOCAL shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if the Session shopping cart exists. If so, use it to populate the local version
            if (Session["cart"] != null)
            {
                //Session shopping cart exists. Put its items in the local version, which is easier to work with
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //We need to UNBOX the Session object to its smaller, more specific type -- Explicit casting
            }
            else
            {
                //If the Session cart doesn't exist yet, we need to instantiate it to get started
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }//After this if/else, we now have a local cart that's ready to add things to it

            //Find the product they referenced by its ID
            Record product = db.Records.Where(b => b.RecordID == recordID).FirstOrDefault();

            if (product == null)
            {
                //If given a bad ID, return the user to some other page to try again.
                //Alternatively, we could throw some kind of error, which we will 
                //discuss further in Module 6.
                return RedirectToAction("Index");
            }
            else
            {
                //If the Book IS valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //Put the item in the local cart. If they already have that product as a
                //cart item, the instead we will update the quantity. This is a big part
                //of why we have the dictionary.
                if (shoppingCart.ContainsKey(product.RecordID))
                {
                    shoppingCart[product.RecordID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.RecordID, item);
                }

                //Now update the SESSION version of the cart so we can maintain that info between requests
                Session["cart"] = shoppingCart; //No explicit casting needed here

            }

            //Send them to View their Cart Items
            return RedirectToAction("Index", "ShoppingCart");

        }

        #endregion

        // GET: Records/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "FirstName");
            ViewBag.StockID = new SelectList(db.StockStatuses, "StockID", "Status");
            return View();
        }

        #region Original Create


         //POST: Records/Create
         //To protect from overposting attacks, please enable the specific properties you wanttobind to, for 
         //more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RecordID,RecordName,BandMusician,GenreID,ReleaseDate,Price,CoverImage,StockID,Producer,CategoryID,ColoredLP,Logo")] Record record, HttpPostedFileBase logo)

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
                    //and check that the file size is <= 4MB, which is default maximum forASP.NET

                    if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                    {
                        //Create a new filename (using a GUID)
                        file = Guid.NewGuid() + ext;
                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/RecordCovers/");

                        Image convertedImage = Image.FromStream(logo.InputStream);

                        int maxImageSize = 300;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage,maxImageSize,maxThumbSize);

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

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID","CategoryName",record.CategoryID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", record.GenreID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID","FirstName",record.ProducerID);
            ViewBag.StockID = new SelectList(db.StockStatuses, "StockID","Status",record.StockID);
            return View(record);
        }
        #endregion

        //#region AJAX CREATE

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult AjaxCreate(Record record)
        //{
        //    db.Records.Add(record);
        //    db.SaveChanges();
        //    return Json(record);
        //}
        //#endregion

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

        #region Original Delete

        //GET: Records/Delete/5
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
        #endregion

        //#region AJAX Delete

        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult AjaxDelete(int id)
        //{
        //    Record record = db.Records.Find(id);
        //    db.Records.Remove(record);
        //    db.SaveChanges();

        //    string confirmMessage = string.Format("Deleted Record '{0}' from the database.", record.RecordName);
        //    return Json(new { id = id, message = confirmMessage });
        //}
        //#endregion

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
