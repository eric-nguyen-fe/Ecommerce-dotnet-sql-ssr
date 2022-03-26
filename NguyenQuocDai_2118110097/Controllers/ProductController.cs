using NguyenQuocDai_2118110097.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenQuocDai_2118110097.Controllers
{
    public class ProductController : Controller
    {
        WebbanhangEntities2 objWebbanhangEntities1 = new WebbanhangEntities2();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebbanhangEntities1.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
       
    }
}