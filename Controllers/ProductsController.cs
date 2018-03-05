using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecomapp;
using ecomapp.Models;

namespace ecomapp.Controllers
{
    public class ProductsController : Controller
    {
        private wedDbcontext db = new wedDbcontext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);



            return View(products.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int ProductId)
        {
            Product productData = db.Products.Find(ProductId);

            TempData["myCartItem"] = productData;

            return RedirectToAction("CartAjax");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NonAction]
        public ActionResult AddtoCartAjax(int ProductId)
        {
            Product productData = db.Products.Find(ProductId);

            TempData["myCartItem"] = productData;

            //return RedirectToAction("CartAjax");
            return null;
        }



        public ActionResult Cart()
        {
            Product pData = TempData["myCartItem"] as Product;
            CartViewModel cartItem = new CartViewModel();

            if (pData != null)
            {
                cartItem.ItemId = pData.ProductId;
                cartItem.ItemName = pData.ProductName;
                cartItem.ItemUnitPrice = pData.ProductPrice;
                cartItem.ItemQuantity = 1;
                cartItem.ItemTotalPrice = pData.ProductPrice;
                cartItem.ItemImagePath = pData.ProductImagePath;
            }

            List<CartViewModel> cList = new List<CartViewModel>();
            if (Session["myCartData"] != null)
            {
                cList = Session["myCartData"] as List<CartViewModel>;
            }

            cList.Add(cartItem);

            //decimal? cItemTotal = 0;
            //for (var i = 0; i < cList.Count; i++)
            //{               
            //   cItemTotal = cItemTotal + cList[i].ItemTotalPrice;                
            //}

            Session["myCartData"] = cList;

            return View(cList);
        }



        public ActionResult AddToCartAjax(int ProductId)
        {
            Product productData = db.Products.Find(ProductId);
            TempData["myCartItem"] = productData;

            Product pData = TempData["myCartItem"] as Product;
            CartViewModel cartItem = new CartViewModel();

            if (pData != null)
            {
                cartItem.ItemId = pData.ProductId;
                cartItem.ItemName = pData.ProductName;
                cartItem.ItemUnitPrice = pData.ProductPrice;
                cartItem.ItemQuantity = 1;
                cartItem.ItemTotalPrice = pData.ProductPrice;
                cartItem.ItemImagePath = pData.ProductImagePath;
            }

            List<CartViewModel> cList = new List<CartViewModel>();
            if (Session["myCartData"] != null)
            {
                cList = Session["myCartData"] as List<CartViewModel>;
            }

            cList.Add(cartItem);

            //decimal? cItemTotal = 0;
            //for (var i = 0; i < cList.Count; i++)
            //{               
            //   cItemTotal = cItemTotal + cList[i].ItemTotalPrice;                
            //}

            Session["myCartData"] = cList;
            return null;

        }

  


            public ActionResult CartAjax()
        {
            Product pData = TempData["myCartItem"] as Product;
            CartViewModel cartItem = new CartViewModel();

            if (pData != null)
            {
                cartItem.ItemId = pData.ProductId;
                cartItem.ItemName = pData.ProductName;
                cartItem.ItemUnitPrice = pData.ProductPrice;
                cartItem.ItemQuantity = 1;
                cartItem.ItemTotalPrice = pData.ProductPrice;
                cartItem.ItemImagePath = pData.ProductImagePath;
            }

            List<CartViewModel> cList = new List<CartViewModel>();
            if (Session["myCartData"] != null)
            {
                cList = Session["myCartData"] as List<CartViewModel>;
            }

            cList.Add(cartItem);

            //decimal? cItemTotal = 0;
            //for (var i = 0; i < cList.Count; i++)
            //{               
            //   cItemTotal = cItemTotal + cList[i].ItemTotalPrice;                
            //}

            Session["myCartData"] = cList;
            TempData["myCartData"] = cList;

            return View(cList);
        }

           
        public ActionResult DeleteItem(int id)
        {
            List<CartViewModel> cList = new List<CartViewModel>();
            if (TempData["myCartData"] != null)
            {
                cList = TempData["myCartData"] as List<CartViewModel>;
            }
            var ci = cList.Find(c => c.ItemId==id);
            
            cList.Remove(ci);
            TempData["myCartData"] = cList;
            //   return RedirectToAction("Cart");

            return Json(cList);
        }

        public ActionResult DeleteItemCart(int id)
        {
            List<CartViewModel> cList = new List<CartViewModel>();
            if (TempData["myCartData"] != null)
            {
                cList = TempData["myCartData"] as List<CartViewModel>;
            }
            var ci = cList.Find(c => c.ItemId == id);

            cList.Remove(ci);
            TempData["myCartData"] = cList;
            return RedirectToAction("Cart");

          //  return Json(cList);
        }


        // GET: Products
        public ActionResult Store()
        {
            var products = db.Products.Include(p => p.Category);

            List<Product> pList = new List<Product>();


            pList = products.ToList();

            //for (var i = 0; i < pList.Count; i++)
           // {
             //   string pItem;
               // pItem = pList[i].ProductImagePath;
                //pItem = Server.MapPath(pItem);
                //pList[i].ProductImagePath = pItem;
            //}

            return View(pList);
        }


        public ActionResult StoreAjax()
        {
            var products = db.Products.Include(p => p.Category);

            List<Product> pList = new List<Product>();


            pList = products.ToList();

            //for (var i = 0; i < pList.Count; i++)
            // {
            //   string pItem;
            // pItem = pList[i].ProductImagePath;
            //pItem = Server.MapPath(pItem);
            //pList[i].ProductImagePath = pItem;
            //}

            return View(pList);
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,CategoryId,ProductName,ProductPrice, ProductImagePath")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CategoryId,ProductName,ProductPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
