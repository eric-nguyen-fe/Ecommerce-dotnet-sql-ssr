using NguyenQuocDai_2118110097.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NguyenQuocDai_2118110097.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        WebbanhangEntities2 objWebbanhang1Entities = new WebbanhangEntities2();
        // GET: Admin/Slider
        public ActionResult Index()
        {
            var lstSlider = objWebbanhang1Entities.Users.ToList();
            return View(lstSlider);
        }
        //GET: Register
        public ActionResult Create()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebbanhang1Entities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objWebbanhang1Entities.Configuration.ValidateOnSaveEnabled = false;
                    objWebbanhang1Entities.Users.Add(_user);
                    objWebbanhang1Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Delete(int iduser)
        {
            var objUser = objWebbanhang1Entities.Users.Where(n => n.IdUser == iduser).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(User objuser)
        {
            var objUser = objWebbanhang1Entities.Users.Where(n => n.IdUser == objuser.IdUser).FirstOrDefault();

            objWebbanhang1Entities.Users.Remove(objUser);
            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}