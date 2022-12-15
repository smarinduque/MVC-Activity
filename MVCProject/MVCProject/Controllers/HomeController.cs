using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /*public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/
        public ActionResult Contact(String orderno, String orderdate, String customername, String totalamount)
        {
            ViewBag.on = string.Format("{0}", orderno);
            ViewBag.od = orderdate;
            ViewBag.cn = customername;
            ViewBag.ta = totalamount;
            return View();
        }
        public ActionResult Customer() 
        {
            return View();
        }
    }
}