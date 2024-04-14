using nhom8_ecommerce.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nhom8_ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        WebsitebanhangEntities2 objWebbanhangEntities1 = new WebsitebanhangEntities2();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objWebbanhangEntities1.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objWebbanhangEntities1.Products.Where(n => n.Categoryid == Id).ToList();
            return View(listProduct);
        }

        
    }
}