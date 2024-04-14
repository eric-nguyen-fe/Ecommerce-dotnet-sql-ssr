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
using static nhom8_ecommerce.Common;

namespace nhom8_ecommerce.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebsitebanhangEntities2 objWebbanhang1Entities = new WebsitebanhangEntities2();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstProduct = new List<Product>();

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
                lstProduct = objWebbanhang1Entities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objWebbanhang1Entities.Products.ToList();
            }

            // Khởi tạo đối tượng SqlCommand để gọi function
            var totalAvailableProducts = objWebbanhang1Entities.Database.SqlQuery<int>("SELECT dbo.GetTotalAvailableProduct() AS TotalAvailableProductCount").FirstOrDefault();
            //GetProductCount
            var totalAvailableProduct = objWebbanhang1Entities.Database.SqlQuery<int>("SELECT dbo.GetProductCount() AS ProductCount").FirstOrDefault();
            ViewBag.GetProductCount = totalAvailableProduct;
            ViewBag.TotalAvailableProducts = totalAvailableProducts;
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]

        public ActionResult Create()
        {
            Common objCommon = new Common();
            //Lấy dữ liệu danh mục dưới DB
            var lstCat = objWebbanhang1Entities.Categories.ToList();
            //Convert sang list dạng value, text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //lấy dl thương hiệu dưới DB
            var lstBrand = objWebbanhang1Entities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dangj value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá ";
            lstProductType.Add(objProductType);


            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất ";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //convert sang select list dangj value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");

            return View();
        }
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objproduct)
        {
            if (objproduct.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload.FileName);
                String extension = Path.GetExtension(objproduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objproduct.Img = fileName;
                objproduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
            }

            objproduct.Available = 199;


            objWebbanhang1Entities.Database.ExecuteSqlCommand("EXEC sp_CreateProductItem @Name, @Img, @Categoryid, @producter, @specication, @origin, @ShortDes, @FullDes, @Price, @PriceDiscount, @discount, @Available, @Typeid, @Bandld, @CreatedOnUtc, @UpdateOnUtc", 
                new SqlParameter("@Name", objproduct.Name),
                new SqlParameter("@Img", objproduct.Img),
                new SqlParameter("@Categoryid", objproduct.Categoryid),
                new SqlParameter("@producter", objproduct.producter),
                new SqlParameter("@specication", objproduct.specication),
                new SqlParameter("@origin", objproduct.origin),
                new SqlParameter("@ShortDes", objproduct.ShortDes ?? ""),
                new SqlParameter("@FullDes", objproduct.FullDes ?? ""),
                new SqlParameter("@Price", objproduct.Price),
                new SqlParameter("@PriceDiscount", objproduct.PriceDiscount),
                new SqlParameter("@discount", objproduct.discount),
                new SqlParameter("@Available", objproduct.Available),
                new SqlParameter("@Typeid", objproduct.Typeid),
                new SqlParameter("@Bandld", objproduct.Bandld),
                new SqlParameter("@CreatedOnUtc", DateTime.UtcNow),
                new SqlParameter("@UpdateOnUtc", DateTime.UtcNow)
            );

            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var objProduct = objWebbanhang1Entities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }


        public ActionResult Delete(int id)
        {
            var objProduct = objWebbanhang1Entities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]

        public ActionResult Delete(Product objproduct)
        {
            var objProduct = objWebbanhang1Entities.Products.Where(n => n.Id == objproduct.Id).FirstOrDefault();

            objWebbanhang1Entities.Database.ExecuteSqlCommand("EXEC sp_DeleteProductItem @ProductId",
                   new SqlParameter("@ProductId", objproduct.Id));
            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]

        public ActionResult Edit(int id)
        {
            var objProduct = objWebbanhang1Entities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]

        public ActionResult Edit(Product objproduct)
        {
            if (objproduct.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload.FileName);
                String extension = Path.GetExtension(objproduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objproduct.Img = fileName;
                objproduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
            }

            objproduct.Available = 199;
            var ProductUpdate = objWebbanhang1Entities.Products.Where(n => n.Id == objproduct.Id).FirstOrDefault();

            objWebbanhang1Entities.Database.ExecuteSqlCommand("EXEC sp_UpdateProductItem @Id, @Name, @Img, @Categoryid, @producter, @specication, @origin, @ShortDes, @FullDes, @Price, @PriceDiscount, @discount, @Available, @Typeid, @Bandld, @CreatedOnUtc, @UpdateOnUtc",
                new SqlParameter("@Id", objproduct.Id),
                new SqlParameter("@Name", objproduct.Name ?? ProductUpdate.Name),
                new SqlParameter("@Img", objproduct.Img ?? ProductUpdate.Img),
                new SqlParameter("@Categoryid", objproduct.Categoryid ?? ProductUpdate.Categoryid),
                new SqlParameter("@producter", objproduct.producter ?? ProductUpdate.producter),
                new SqlParameter("@specication", objproduct.specication ?? ProductUpdate.specication ),
                new SqlParameter("@origin", objproduct.origin ?? ProductUpdate.origin ),
                new SqlParameter("@ShortDes", objproduct.ShortDes ?? ProductUpdate.ShortDes),
                new SqlParameter("@FullDes", objproduct.FullDes ?? ProductUpdate.FullDes),
                new SqlParameter("@Price", objproduct.Price ?? ProductUpdate.Price ),
                new SqlParameter("@PriceDiscount", objproduct.PriceDiscount ?? ProductUpdate.PriceDiscount ),
                new SqlParameter("@discount", objproduct.discount ?? ProductUpdate.discount),
                new SqlParameter("@Available", objproduct.Available ?? ProductUpdate.Available ),
                new SqlParameter("@Typeid", objproduct.Typeid ?? ProductUpdate.Typeid   ),
                new SqlParameter("@Bandld", objproduct.Bandld ?? ProductUpdate.Bandld ),
                new SqlParameter("@CreatedOnUtc", ProductUpdate.CreatedOnUtc ?? DateTime.UtcNow),
                new SqlParameter("@UpdateOnUtc", DateTime.UtcNow)
            );

            objWebbanhang1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}