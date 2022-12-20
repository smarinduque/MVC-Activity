using MVCProject.Context;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class OrdersController : Controller
    {
        CustomerContext cc = new CustomerContext();
        // GET: Orders
        public ActionResult Order()
        {
            ViewBag.Tittle = "List Of Orders";
            var customers = cc.Customers.ToList();
            var model = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                ContactName = c.ContactName
            }).ToList();

            SelectList list = new SelectList(model, "Id", "ContactName");
            ViewData["CustomerList"] = list;
            return View();
        }
        [HttpGet]
        public ActionResult AddOrder() 
        {
            ViewBag.Tittle = "Order Details";
            var customers = cc.Customers.ToList();
            var dropdownlist = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                ContactName = c.ContactName
            }).ToList();
            SelectList list = new SelectList(dropdownlist,"Id","ContactName");
            ViewData["CustomerId"] = list;

            //OrderDTO odto = new OrderDTO();
            return View();
        }
        [HttpPost]
        public ActionResult AddOrder(OrderDTO data) 
        {
            using (var cc = new CustomerContext()) 
            {
                var order_items = data.Items.Select(s => new Order_items
                {
                    Id = s.Id,
                    OrderId = s.OrderId,
                    ProductId = s.ProductId,
                    ProductCode = s.ProductCode,
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    Amount = s.Amount
                }).ToList();
                var order = new Order
                {
                    Id = data.Id,
                    OrderNo = data.OrderNo,
                    OrderDate = data.OrderDate,
                    CustomerId = data.CustomerId,
                    TotalAmount = (double)order_items.Sum(x => x.Amount),
                    Items = order_items
                };
                cc.Orders.Add(order);
                cc.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductModal() 
        {  
            var model = new Order_itemsDTO();
            var products = cc.Products.ToList();
            var productlist = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Price = p.Price
            });
            SelectList productlists = new SelectList(productlist, "Id", "Name");
            ViewData["ProductId"] = productlists;
            return PartialView("ModalProduct",model);
        }
        public ActionResult GetValueOnchangeProduct(int Id)
        {
            var product = cc.Products.FirstOrDefault(p => p.Id == Id);
            var productModel = new ProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price

            };
            return Json(productModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _OrderDetails(int id = 0)
        {
            var customers = cc.Customers.ToList();
            var customerModel = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                ContactName = c.FirstName + " " +c.MiddleName +" "+ c.LastName,
                FirstName = c.FirstName,
                LastName = c.LastName,
                MiddleName = c.MiddleName,
                Gender = c.Gender,
                LocalAddress = c.LocalAddress,
                TelNo = c.TelNo,
                MobileNO = c.MobileNO,
                IsActive = c.IsActive,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            }).ToList();

            var orderList = cc.Orders.Where(x => x.CustomerId == id || id == 0).ToList();
            var modelOrder = orderList.Select(c => new OrderDTO
            {
                Id = c.Id,
                OrderNo = c.OrderNo,
                OrderDate = c.OrderDate,
                CustomerId = c.CustomerId,
                TotalAmount = c.TotalAmount,
                customer = customerModel.FirstOrDefault(x => x.Id == c.CustomerId)
            }).ToList();



            return PartialView("OrderDetails", modelOrder);
        }
        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            var order = cc.Orders.FirstOrDefault(c => c.Id == id);
            order.Items = cc.OrdersItems.Where(x => x.OrderId == id).ToList();

            var order_item = order.Items.Select(s => new Order_itemsDTO
            {
                Id = s.Id,
                OrderId = s.OrderId,
                ProductId = s.ProductId,
                ProductCode = s.ProductCode,
                ProductName = s.ProductName,
                Quantity = s.Quantity,
                Price = s.Price,
                Amount = s.Amount,
            }).ToList();

            var model = new OrderDTO
            {
                Id = order.Id,
                OrderNo = order.OrderNo,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerId = order.CustomerId,
                Items = order_item
            };

            var customers = cc.Customers.ToList();
            var customerModel = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                ContactName = c.FirstName+ " " + c.MiddleName + " " +c.LastName 
            }).ToList();
            SelectList CustomerList = new SelectList(customerModel, "Id", "ContactName", model.CustomerId);
            ViewData["CustomerId"] = CustomerList;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditOrder(OrderDTO data)
        {
            using (var cc = new CustomerContext())
            {
                var order_item = data.Items.Select(s => new Order_items
                {
                    Id = s.Id,
                    OrderId = data.Id,
                    ProductId = s.ProductId,
                    ProductCode = s.ProductCode,
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    Amount = s.Amount,

                }).ToList();
                var order = new Order
                {
                    Id = data.Id,
                    OrderNo = data.OrderNo,
                    OrderDate = data.OrderDate,
                    CustomerId = data.CustomerId,
                    TotalAmount = (double)order_item.Sum(x => x.Amount)
                };
                var toDelete = cc.OrdersItems.Where(x => x.OrderId == order.Id).ToList();
                cc.OrdersItems.RemoveRange(toDelete);

                cc.OrdersItems.AddRange(order_item);

                var entity = cc.Orders.FirstOrDefault(x => x.Id == data.Id);
                cc.Entry(entity).CurrentValues.SetValues(order);
                cc.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}