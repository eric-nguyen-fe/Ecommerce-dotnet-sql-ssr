﻿using nhom8_ecommerce.ContactDB;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nhom8_ecommerce.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WebsitebanhangEntities2 objWebbanhang1Entities = new WebsitebanhangEntities2();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstBrand = new List<Brand>();

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
                lstBrand = objWebbanhang1Entities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objWebbanhang1Entities.Brands.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objbrand)
        {
            try
            {
                if (objbrand.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objbrand.ImageUpload.FileName);
                    String extension = Path.GetExtension(objbrand.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objbrand.Img = fileName;
                    objbrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
                }
                objWebbanhang1Entities.Brands.Add(objbrand);
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
            var objbrand = objWebbanhang1Entities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objbrand);
        }
        public ActionResult Delete(int id)
        {
            var objBrand = objWebbanhang1Entities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objbrand)
        {
            var objBrand = objWebbanhang1Entities.Brands.Where(n => n.Id == objbrand.Id).FirstOrDefault();

            objWebbanhang1Entities.Brands.Remove(objbrand);
            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objWebbanhang1Entities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Brand objbrand)
        {
            if (objbrand.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objbrand.ImageUpload.FileName);
                String extension = Path.GetExtension(objbrand.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objbrand.Img = fileName;
                objbrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
            }
            objWebbanhang1Entities.Entry(objbrand).State = System.Data.Entity.EntityState.Modified;
            objWebbanhang1Entities.SaveChanges();
            return View(objbrand);

        }
    }
}