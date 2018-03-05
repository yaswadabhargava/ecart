using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecomapp.Models
{
    public class CartViewModel
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemImagePath { get; set; }
        public decimal? ItemUnitPrice { get; set; }
        public int ItemQuantity { get; set; }
        public decimal? ItemTotalPrice { get; set; }

    }
}