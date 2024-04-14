using nhom8_ecommerce.ContactDB;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace nhom8_ecommerce.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        WebsitebanhangEntities2 objWebbanhang1Entities = new WebsitebanhangEntities2();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var lstProduct = new List<Product>();

            return View(lstProduct);
        }
        public ActionResult afff()
        {
            throw new NotImplementedException();
        }

        public ActionResult RefeshCard()
        {
            /*if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objWebbanhangEntities1.Products.Find(Id), Quantity = Quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(Id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += Quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { Product = objWebbanhangEntities1.Products.Find(Id), Quantity = Quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }*/
            return Json(new { Message = "Thành công" });
        }
    }
}