using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.UI.MVC.Exceptions;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class CustomErrorsController : Controller
    {
        // GET: CustomErrors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SampleError()
        {
            int x = 0;
            int y = 42;
            int z = y / x;

            return View();
        }

        public ActionResult Throw404()
        {
            return HttpNotFound();
        }

        public ActionResult Unresolved()
        {
            return View();
        }

        public ActionResult DBTest()
        {

            try
            {
                bool dbCheck = true; 

                if (dbCheck)
                {
                    return View();
                }
                else
                {
                    throw new DBOfflineException();
                }

            }
            catch (Exception)
            {

                return RedirectToAction("DBOffline"); 
            }

        }

        public ActionResult DBOffline()
        {
            return View();
        }

    }
}