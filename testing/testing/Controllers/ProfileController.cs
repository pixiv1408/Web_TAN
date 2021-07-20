using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testing.Models;

namespace testing.Controllers
{
    public class ProfileController : Controller
    {
        Database db = new Database();
        // GET: Profile
        public ActionResult profile()//hàm lấy thông tin người dùng 
        {
            if (Session["LogIn"] == null)
            {
                return RedirectToAction("DangNhap", "Customer");
            }
            Customer kh = Session["KH"] as Customer;
            return View(kh);
        }
        [HttpPost]
        public ActionResult profile(FormCollection form)//hàm cập nhật thông tin người dùng
        {
            Customer kh = Session["KH"] as Customer;
            Account ac = Session["LogIn"] as Account;
            if (kh == null)
            {
                return RedirectToAction("DangNhap", "Customer");
            }
            if (string.IsNullOrEmpty(form["email"]))
            {
                ViewData["loiemail"] = "Email không được bỏ trống";
                ViewData["ketqua"] = "Cập nhật thất bại";
            }
            else if (string.IsNullOrEmpty(form["phone"]))
            {
                ViewData["loiphone"] = "Số điện thoại không được bỏ trống";
                ViewData["ketqua"] = "Cập nhật thất bại";
            }
            else if (string.IsNullOrEmpty(form["address"]))
            {
                ViewData["loidc"] = "Địa chỉ không hợp lệ";
                ViewData["ketqua"] = "Cập nhật thất bại";
            }
            else if (!form["pass"].Equals(ac.Password))
            {
                ViewData["loipass"] = "Mật khẩu không chính xác";
                ViewData["ketqua"] = "Cập nhật thất bại";
            }
            else
            {
                Customer update = db.Customers.FirstOrDefault(p => p.CusID == kh.CusID);
                update.Gmail = form["email"];
                update.Phone = form["phone"];
                update.Address = form["address"];
                db.SaveChanges();
                ViewData["ketqua"] = "Cập nhật thành công";
                Session["KH"] = update;
            }
            return profile();
        }
    }
}