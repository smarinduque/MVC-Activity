using MVCProject.Context;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class CustomersController : Controller
    {
        CustomerContext cc = new CustomerContext();
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer()
        {
            var list = cc.Customers.ToList();
            var model = list.Select(c => new CustomerDTO
            {
                Id = c.Id,
                ContactName = c.ContactName,
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

            return View(model);
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            ViewBag.Tittle = "Customer Details";
            CustomerDTO cdto = new CustomerDTO();
            return View(cdto);
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerDTO data)
        {
            //var list = cc.Customers.ToArray();
            var customer = new Customer
            {
                Id = data.Id,
                ContactName = data.FirstName + " " + data.MiddleName + " " + data.LastName,
                FirstName = data.FirstName,
                LastName = data.LastName,
                MiddleName = data.MiddleName,
                Gender = data.Gender,
                LocalAddress = data.LocalAddress,
                TelNo = data.TelNo,
                MobileNO = data.MobileNO,
                IsActive = data.IsActive,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.CreatedDate
            };
            cc.Customers.Add(customer);
            cc.SaveChanges();
            return RedirectToAction("Customer");
        }
        [HttpGet]
        public ActionResult EditCustomer(int id) 
        {
            ViewBag.Tittle = "Customer Details";
            Customer model = new Customer();
            model = cc.Customers.FirstOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer data) 
        {
            using (var db = new CustomerContext()) 
            {
                var entity = db.Customers.FirstOrDefault(x => x.Id == data.Id);
                data.ContactName = data.FirstName + " " + data.MiddleName + " " + data.LastName; 
                db.Entry(entity).CurrentValues.SetValues(data);
                db.SaveChanges();
            }
            return RedirectToAction("Customer");
        }
    }
}