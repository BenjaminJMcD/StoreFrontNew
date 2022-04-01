using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.UI.MVC.Models;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            //Create a local version of the shopping cart from session(global) 
            //if the value is null or count is 0 create an empty instance and provide no car items verbiage
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
                ViewBag.Message = "There are no records in your cart";
            }
            //if cart isnt null and count <0 null the message
            else
            {
                ViewBag.Message = null;
            }

            //return the view with the cart
            return View(shoppingCart);
        }

        public ActionResult UpdateCart(int recordID, int qty)
        {
            //if they zero out hte qty from the update, remove from the cart
            if (qty == 0)
            {
                RemoveFromCart(recordID);

                return RedirectToAction("Index");
            }

            //retrieve the cart from session and assign it to the local dictionaryt
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //update the qty in the LOCAL storage
            shoppingCart[recordID].Qty = qty;

            //return the Local cart to session (GLOBAL)
            Session["cart"] = shoppingCart;

            //logic to dicplay a message if they update to NO items in their cart
            if (shoppingCart.Count == 0)
            {
                ViewBag.Message = "There are no records in your cart";
            }

            //redirect to the Index - We nee the logic in the index ACTION to run so just returning to the view will not work
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //cart out of session and into the local
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //call the remove() from the dictionary class
            shoppingCart.Remove(id);

            //null the session to hide the cart link when session is empty
            if (shoppingCart.Count == 0)
            {
                Session["cart"] = null;
            }

            //redirect back to the index action (running all of code and repopulating the table or displaying the count message
            return RedirectToAction("Index");
        }
    }
}