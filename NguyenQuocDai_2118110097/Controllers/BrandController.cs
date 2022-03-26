using NguyenQuocDai_2118110097.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenQuocDai_2118110097.Controllers
{
    public class BrandController : Controller
    {
        WebbanhangEntities2 objWebbanhangEntities1 = new WebbanhangEntities2();
        // GET: Brand
        public ActionResult ProductBrands(int Id)
        {
            var listProduct = objWebbanhangEntities1.Products.Where(n => n.Bandld == Id).ToList();
            return View(listProduct);
        }

    }
}