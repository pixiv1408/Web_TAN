using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testing.Models;
using PagedList;
using PagedList.Mvc;
namespace testing.Controllers
{
    public class HistoryOrderController : Controller
    {
        // GET: HistoryOrder
        Database db = new Database();

        public ActionResult HistoryOrder(int? page)//lấy ds hóa đơn theo tài khoản đang đăng nhập
        {
            Account acc = Session["LogIn"] as Account;
            //List<Bill> objbill;
            int pageNum = (page ?? 1);
            int pageSize = 6;
            if (acc != null)
            {
                var objbill = db.Bills.Where(p => p.CusID == acc.CusID).ToList().ToPagedList(pageNum,pageSize);
                return View(objbill);
            }
            return RedirectToAction("DangNhap", "Customer");
        }
     
        [HttpGet]
        public ActionResult DetailHistory(int? id)//liệt kê ds món ăn đã mua theo mã hóa đơn truyền vào
        {
            //var id1 = id;
            Account acc = Session["LogIn"] as Account;
            if (acc == null)
            {
                return RedirectToAction("DangNhap", "Customer");
            }
            else
            {
                var detail = db.Details.Where(p => p.DOderID == id).ToList();
                return View(detail);
            }
        }
    }
}