using NguyenQuocDai_2118110097.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenQuocDai_2118110097.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        WebbanhangEntities2 objWebbanhang1Entities = new WebbanhangEntities2();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var lstProduct = objWebbanhang1Entities.Products.ToList();
                return View(lstProduct);
            }
            else
            {
                return View("Login");
            }
            //return View();
        }
    }
}