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
    public class CategoriesController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Categories
        public ActionResult Local(int page = 1)
        {
            int pageSize = 4;


            ViewBag.SearchFilter = new SelectList(db.Records.Select(x => x.Category).Distinct());

            var records = db.Records.Include(r => r.Category).Include(r => r.Genre).Include(r => r.Producer).Include(r => r.StockStatus).OrderBy(r => r.BandMusician).ToList();
            //return View(records.ToList());

            return View(records.ToPagedList(page, pageSize));

        }

        public ActionResult Wizardry()
        {
            
            return View();
        }

        public ActionResult Backyard()
        {
            return View();
        }
    }
}