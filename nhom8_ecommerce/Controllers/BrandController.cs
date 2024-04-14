using nhom8_ecommerce.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nhom8_ecommerce.Controllers
{
    public class BrandController : Controller
    {
        WebsitebanhangEntities2 objWebbanhangEntities1 = new WebsitebanhangEntities2();
        // GET: Brand
        public ActionResult ProductBrands(int Id)
        {
            var listProduct = objWebbanhangEntities1.Products.Where(n => n.Bandld == Id).ToList();
            return View(listProduct);
        }

    }
}