using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testing.Models;
using testing.ViewModels;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Collections.Concurrent;


namespace testing.Controllers
{
    public class AdminController : Controller
    {
        Database db = new Database();
        // GET: Admin
        public ActionResult IndexAD()
        {
            if(Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }   
        //Login AdMin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var usern = form["username"];
            var pass = form["password"];
            if (ModelState.IsValid)
            {
                Admin ad = db.Admins.SingleOrDefault(x => x.UserAdmin == usern && x.PassAdmin == pass);
                if (ad != null)
                {
                    Session["LoginAD"] = ad;
                    return RedirectToAction("IndexAD", "Admin");
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không chính xác";
            }
            return View();
        }      
    }
}