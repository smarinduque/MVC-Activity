using MVCProject.Context;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class ProductsController : Controller
    {
        CustomerContext cc = new CustomerContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product() 
        {
            ViewBag.Tittle = "List of Product";
            var list = cc.Products.ToList();
            var model = list.Select(p => new ProductDTO
            {
                Code = p.Code,
                Name = p.Name,
                StockOnHand = p.StockOnHand,
                Price = p.Price
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Tittle = "Product Details";
            ProductDTO pdto = new ProductDTO();
            return View(pdto);        
        }
        [HttpPost]
        public ActionResult AddProduct(ProductDTO data) 
        {
            var product = new Product
            {
                Id = data.Id,
                Code = data.Code,
                Name = data.Name,
                StockOnHand = data.StockOnHand,
                Price = data.Price
            };
            cc.Products.Add(product);
            cc.SaveChanges();
            return RedirectToAction("Product");
        }
        [HttpGet]
        public ActionResult EditProduct(int Id) 
        {
            ViewBag.Tittle = "Product Details";
            Product model = new Product();
            model = cc.Products.FirstOrDefault(m => m.Id == Id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProduct(Product data) 
        {
            using (var db = new CustomerContext()) 
            {
                var entity = db.Products.FirstOrDefault(p => p.Code == data.Code);
                db.Entry(entity).CurrentValues.SetValues(data);
                db.SaveChanges();
            }
                return RedirectToAction("Product");
        }
    }
}