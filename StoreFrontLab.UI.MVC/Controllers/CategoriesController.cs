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


            //ViewBag.SearchFilter = new SelectList(db.Records.Select(x => x.Category.CategoryID ==1));

            
            

            var records = db.Records.Where(a => a.CategoryID.Value == 1).ToList();
            //return View(records.ToList());

            


            return View(records.ToPagedList(page, pageSize));

        }

        public ActionResult Wizardry(int page = 1)
        {
            int pageSize = 4;

            var records = db.Records.Where(a => a.CategoryID.Value == 2).ToList();

            return View(records.ToPagedList(page, pageSize));
        }

        public ActionResult Backyard(int page = 1)
        {
            int pageSize = 4;

            var records = db.Records.Where(a => a.CategoryID.Value == 3).ToList();

            return View(records.ToPagedList(page, pageSize));
        }
    }
}