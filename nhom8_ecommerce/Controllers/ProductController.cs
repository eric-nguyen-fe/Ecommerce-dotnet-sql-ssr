using nhom8_ecommerce.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nhom8_ecommerce.Controllers
{
    public class ProductController : Controller
    {
        WebsitebanhangEntities2 objWebbanhangEntities1 = new WebsitebanhangEntities2();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebbanhangEntities1.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
       
    }
}