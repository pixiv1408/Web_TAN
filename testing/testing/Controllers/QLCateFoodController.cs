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
    public class QLCateFoodController : Controller
    {
        Database db = new Database();
        // GET: QLCateFood
        public ActionResult Index()
        {
            return View();
        }
        // show list loại món
        public ActionResult LoaiMon()
        {
            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.Categories.ToList().OrderBy(x => x.CateID));
            }
        }

        // thêm loại món
        [HttpGet]
        public ActionResult ThemLoaiMon()
        {
            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ThemLoaiMon(FormCollection form)
        {
            Category ct = new Category();
            var tenmon = form["cateName"];
            if (ModelState.IsValid)
            {
                var check = db.Categories.SingleOrDefault(a => a.CName == ct.CName);
                if (check == null)
                {
                    ct.CName = tenmon;
                    ct.CStatus = true;
                    db.Categories.Add(ct);
                    db.SaveChanges();
                    return RedirectToAction("LoaiMon", "Admin");
                }
                else
                {
                    ViewBag.Error = "Loại món đã tồn tại";
                    return View();
                }
            }
            return View();
        }
        // xóa và khôi phục loại món
        [HttpPost]
        public ActionResult DeleteCate(int id)
        {
            Category cate = db.Categories.SingleOrDefault(a => a.CateID == id);
            if (cate == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            if (cate.CStatus == true)
            {
                cate.CStatus = false;
            }
            else
            {
                cate.CStatus = true;
            }
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("LoaiMon");
        }
        // Edit loại món
        [HttpGet]
        public ActionResult EditCate(int id)
        {
            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                Category cate = new Category();
                cate = db.Categories.SingleOrDefault(a => a.CateID == id);
                return View(cate);
            }
        }
        [HttpPost]
        public ActionResult EditCate(Category cate)
        {
            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                cate.CStatus = true;
                db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoaiMon");
            }
        }

    }
}