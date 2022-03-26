using NguyenQuocDai_2118110097.ContactDB;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenQuocDai_2118110097.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        WebbanhangEntities2 objWebbanhang1Entities = new WebbanhangEntities2();
        // GET: Admin/Post
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstPost = new List<Post>();

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                lstPost = objWebbanhang1Entities.Posts.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstPost = objWebbanhang1Entities.Posts.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstPost.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Post objpost)
        {
            try
            {
                if (objpost.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objpost.ImageUpload.FileName);
                    String extension = Path.GetExtension(objpost.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objpost.img = fileName;
                    objpost.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
                }
                objWebbanhang1Entities.Posts.Add(objpost);
                objWebbanhang1Entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            var objpost = objWebbanhang1Entities.Posts.Where(n => n.Id == id).FirstOrDefault();
            return View(objpost);
        }
        public ActionResult Delete(int id)
        {
            var objPost = objWebbanhang1Entities.Posts.Where(n => n.Id == id).FirstOrDefault();
            return View(objPost);
        }
        [HttpPost]
        public ActionResult Delete(Post objpost)
        {
            var objPost = objWebbanhang1Entities.Posts.Where(n => n.Id == objpost.Id).FirstOrDefault();

            objWebbanhang1Entities.Posts.Remove(objpost);
            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objPost = objWebbanhang1Entities.Posts.Where(n => n.Id == id).FirstOrDefault();
            return View(objPost);
        }
        [HttpPost]
        public ActionResult Edit(Post objPost)
        {
            if (objPost.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objPost.ImageUpload.FileName);
                String extension = Path.GetExtension(objPost.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objPost.img = fileName;
                objPost.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
            }
            objWebbanhang1Entities.Entry(objPost).State = System.Data.Entity.EntityState.Modified;
            objWebbanhang1Entities.SaveChanges();
            return View(objPost);

        }
    }
}