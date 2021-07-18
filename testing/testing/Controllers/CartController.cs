using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testing.Models;
using testing.ViewModels;

namespace testing.Controllers
{
    public class CartController : Controller
    {
        Database db = new Database();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        // lấy session
        public List<GioHang> listGioHang()
        {
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            if (lstGH == null)
            {
                lstGH = new List<GioHang>();
                Session["GioHang"] = lstGH;
            }

            return lstGH;
        }

        //add cart
        public ActionResult addCart(int IdFood,string strURL)
        {
            List<GioHang> lstGH = listGioHang();
            GioHang monan = lstGH.Find(x => x.FoodID == IdFood);
            if(monan == null)
            {
                monan = new GioHang(IdFood);
                lstGH.Add(monan);
                return Redirect(strURL);
            }
            else
            {
                monan.DAmount++;
                return Redirect(strURL);
            }
        }
        // mua ngay
        public ActionResult MuaNgay(int IdFood)
        {
            List<GioHang> lstGH = listGioHang();
            GioHang monan = lstGH.Find(x => x.FoodID == IdFood);
            if (monan == null)
            {
                monan = new GioHang(IdFood);
                lstGH.Add(monan);
                return RedirectToAction("MuaHang", "Cart");
            }
            else
            {
                monan.DAmount++;
                return RedirectToAction("MuaHang", "Cart");
            }
        }
        // tổng số lượng món
        private int TongSL()
        {
            int iTongSL = 0;
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            if (lstGH != null)
            {
                iTongSL = lstGH.Sum(a => a.DAmount);
            }
            return iTongSL;
        }
        // tổng hóa đơn
        private int TotalPrice()
        {
            int iTotalPrice = 0;
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            if(lstGH != null)
            {
                iTotalPrice = lstGH.Sum(n => n.BTotal);
            }
            return iTotalPrice;
        }

        // giỏ hàng
        public ActionResult GioHang()
        {
            List<GioHang> lstGH = listGioHang();
            if(lstGH.Count == 0)
            {
                return RedirectToAction("Index", "FoodHome");
            }
            ViewBag.TongSL = TongSL();
            ViewBag.TotalPrice = TotalPrice();
            return View(lstGH);
        }

        // show số lượng cart trong header
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult PartialGioHang()
        {
            ViewBag.TongSL = TongSL();
            ViewBag.ToTalPrice = TotalPrice();
            return PartialView();
        }

        // xóa món khỏi giỏ hàng
        public ActionResult DeleteCart(int idFood)
        {
            List<GioHang> lstGH = listGioHang();
            GioHang mon = lstGH.FirstOrDefault(x => x.FoodID == idFood);
            if(mon != null)
            {
                lstGH.RemoveAll(a => a.FoodID == idFood);
                return RedirectToAction("GioHang");
            }
            if(lstGH.Count == 0)
            {
                return RedirectToAction("Index", "FoodHome");
            }
            return RedirectToAction("GioHang");
        }

        // cập nhập số lượng giỏ hàng
       
        public ActionResult UpdateCart(int idFood,FormCollection form)
        {

            List<GioHang> lstGH = listGioHang();
            GioHang mon = lstGH.FirstOrDefault(x => x.FoodID == idFood);
            if (mon != null)
            {
                mon.DAmount = int.Parse(form["qty"].ToString());
            }

            return RedirectToAction("GioHang");
        }
        // mua hàng
        [HttpGet]
        public ActionResult MuaHang()
        {          
            if(Session["LogIn"]== null || Session["LogIn"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Customer");    
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Foodhome");
            }

            List<GioHang> lstGH = listGioHang();
            ViewBag.TongSL = TongSL();
            ViewBag.TotalPrice = TotalPrice();
            return View(lstGH);
        }
        [HttpPost]
        public ActionResult MuaHang(FormCollection form)
        {         
            Bill donhang = new Bill();
            Account acc = (Account)Session["LogIn"];
            List<GioHang> lstGH = listGioHang();
            var note = form["textNote"];
            donhang.CusID = acc.CusID;
            donhang.BDate = DateTime.Now;
            donhang.BCart = true;
            donhang.BTotal = TotalPrice();
            donhang.note = note;
            db.Bills.Add(donhang);
            db.SaveChanges();
            
            foreach(var item in lstGH)
            {
                Detail CTDH = new Detail();
                CTDH.DOderID = donhang.BillID;
                CTDH.DFoodID = item.FoodID;
                CTDH.DFName = item.FName;
                CTDH.DAmount = item.DAmount;
                CTDH.DfPrice = (int)item.FPrice;
                CTDH.DFimg = item.Fimage;
                db.Details.Add(CTDH);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("ThongBaoOrder", "Cart");
        }

        // thông báo mua hàng thành công
        public ActionResult ThongBaoOrder()
        {
            return View();
        }
    }
}