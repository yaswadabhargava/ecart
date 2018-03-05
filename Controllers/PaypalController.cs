﻿using ecomapp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecomapp.Controllers
{
    public class PayPalController : Controller
    {
        public ActionResult RedirectFromPaypal()
        {
            return View();
        }

        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        public ActionResult NotifyFromPaypal()
        {
            return View();
        }

       
        public ActionResult ValidateCommand(string products, string totalPrice)
        {
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["is_sandbox"]);
            var paypal = new PayPalModel(useSandbox);

            paypal.item_name = products;
            paypal.amount = totalPrice;
            return View(paypal);
        }

    }
}