using NguyenQuocDai_2118110097.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenQuocDai_2118110097.Controllers
{
    public class PostController : Controller
    {
        WebbanhangEntities2 objWebbanhangEntities1 = new WebbanhangEntities2();
        // GET: Post
        public ActionResult Index()
        {
            var LstPost = objWebbanhangEntities1.Posts.ToList();
            return View(LstPost);
        }
    }
}