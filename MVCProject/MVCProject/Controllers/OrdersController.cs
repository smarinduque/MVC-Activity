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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
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
    }
}