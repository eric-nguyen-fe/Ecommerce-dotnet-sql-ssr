using nhom8_ecommerce.ContactDB;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nhom8_ecommerce.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        WebsitebanhangEntities2 objWebbanhang1Entities = new WebsitebanhangEntities2();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstCategory = new List<Category>();

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
                lstCategory = objWebbanhang1Entities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objWebbanhang1Entities.Categories.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objcategory)
        {
            try
            {
                if (objcategory.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objcategory.ImageUpload.FileName);
                    String extension = Path.GetExtension(objcategory.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objcategory.Img = fileName;
                    objcategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
                }
                objWebbanhang1Entities.Categories.Add(objcategory);
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
            var objpost = objWebbanhang1Entities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objpost);
        }
        public ActionResult CountAvailableProductsByCategoryId(int categoryId)
        {
            int productCount = 0;

            using (var dbContext = new WebsitebanhangEntities2())
            {
                // Tạo tham số output để lưu trữ kết quả trả về từ store procedure
                var outputParameter = new SqlParameter("@ProductCount", System.Data.SqlDbType.Int);
                outputParameter.Direction = System.Data.ParameterDirection.Output;

                // Thực thi store procedure và lấy kết quả
                dbContext.Database.ExecuteSqlCommand("EXEC CountProductsAvaibleByCategoryId @CategoryId, @ProductCount OUTPUT",
                    new SqlParameter("@CategoryId", categoryId),
                    outputParameter);

                // Lấy giá trị từ tham số output
                productCount = (int)outputParameter.Value;
            }

            // Truyền dữ liệu ra view
            ViewBag.ProductCount = productCount;
            ViewBag.CategoryId = categoryId;

            // Trả về view với dữ liệu đã tính toán
            return View();
        }
        public ActionResult Delete(int id)
        {
            var objPost = objWebbanhang1Entities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objPost);
        }
        [HttpPost]
        public ActionResult Delete(Category objcategory)    
        {
            var objPostItemDelete = objWebbanhang1Entities.Categories.Where(n => n.Id == objcategory.Id).FirstOrDefault();

            objWebbanhang1Entities.Categories.Remove(objPostItemDelete);
            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objItem = objWebbanhang1Entities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objItem);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Category objcategory)
                                                                                                                {
            if (objcategory.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objcategory.ImageUpload.FileName);
                String extension = Path.GetExtension(objcategory.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objcategory.Img = fileName;
                objcategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
            }

            var ProductUpdate = objWebbanhang1Entities.Categories.Where(n => n.Id == objcategory.Id).FirstOrDefault();


            objWebbanhang1Entities.Database.ExecuteSqlCommand("EXEC sp_UpdateCategoryItem @Id, @Name, @Img, @Slug,  @Showonhomepage, @DisplayOrder,  @Deleted, @CreatedOnUtc, @UpdateOnUtc",
                new SqlParameter("@Id", objcategory.Id),
                new SqlParameter("@Name", objcategory.Name ?? ProductUpdate.Name),
                new SqlParameter("@Img", objcategory.Img ?? ProductUpdate.Img),
                new SqlParameter("@Slug", objcategory.Slug ?? ProductUpdate.Slug),
                new SqlParameter("@Showonhomepage", objcategory.Showonhomepage ?? ProductUpdate.Showonhomepage),
                new SqlParameter("@DisplayOrder", objcategory.DisplayOrder ?? ProductUpdate.DisplayOrder),
                new SqlParameter("@Deleted", objcategory.Deleted ?? ProductUpdate.Deleted),
                new SqlParameter("@CreatedOnUtc", ProductUpdate.CreatedOnUtc ?? DateTime.UtcNow),
                new SqlParameter("@UpdateOnUtc", DateTime.UtcNow)
            );

            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}