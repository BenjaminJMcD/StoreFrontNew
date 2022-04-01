using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.DATA.EF;
using System.ComponentModel.DataAnnotations;

namespace StoreFrontLab.UI.MVC.Models
{
    public class CartItemViewModel
    {
        [Range(1,byte.MaxValue,ErrorMessage = "* Please enter a quantity between 1 and 255.")]
        public int Qty { get; set; }

        public Record Product { get; set; }

        public CartItemViewModel(int qty, Record product)
        {
            Qty = qty;
            Product = product;
        }

    }
}