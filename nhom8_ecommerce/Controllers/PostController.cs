using nhom8_ecommerce.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nhom8_ecommerce.Controllers
{
    public class PostController : Controller
    {
        WebsitebanhangEntities2 objWebbanhangEntities1 = new WebsitebanhangEntities2();
        // GET: Post
        public ActionResult Index()
        {
            var LstPost = objWebbanhangEntities1.Posts.ToList();
            return View(LstPost);
        }
    }
}