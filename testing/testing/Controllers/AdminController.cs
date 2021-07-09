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


        // show list món ăn
        public ActionResult MonAn(int ?page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 8;
            //var listFood = db.Foods.ToList();
            //Food ds = new Food();

            return View(db.Foods.ToList().Where(a=>a.FStatus==true).OrderBy(x=>x.FoodID).ToPagedList(pageNum,pageSize));
        }
        // show list loại món
        public ActionResult LoaiMon()
        {
            return View(db.Categories.ToList().Where(a=>a.CStatus==true).OrderBy(x => x.CateID));
        }
        
        // thêm loại món
        [HttpGet]
        public ActionResult ThemLoaiMon()
        {
            return View();
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
        // xóa loại món
        [HttpPost]
        public ActionResult DeleteCate(int id)
        {
            Category cate = db.Categories.SingleOrDefault(a => a.CateID == id);
            if(cate == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            cate.CStatus = false;
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();             
            return RedirectToAction("LoaiMon");
        }

        //Thêm món
        public ActionResult CreateNew()
        {
            Food mon = new Food();
            mon.listCategory = db.Categories.ToList();

            return View(mon);
        }
        [HttpPost]
        public ActionResult CreateNew(Food monan,HttpPostedFileBase fileupload)
        {
            if(fileupload == null)
            {
                ViewBag.ThongBao = "Chọn ảnh món ăn";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);

                    var path = Path.Combine(Server.MapPath("~/images"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    monan.Fimage = fileName;
                    monan.FStatus = true;
                    db.Foods.Add(monan);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("MonAn");
        }
        // chi tiết món
        public ActionResult DetailsFood(int id)
        {
            Food monan = db.Foods.SingleOrDefault(a => a.FoodID == id);

            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(monan);
        }
        // xóa món
        public ActionResult DeleteF(int id)
        {
            Food monan = db.Foods.SingleOrDefault(a => a.FoodID == id);
            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(monan);
        }
        [HttpPost,ActionName("DeleteF")]
        public ActionResult XacNhanXoa(int id)
        {
            Food monan = new Food();
            monan = db.Foods.SingleOrDefault(a => a.FoodID == id);
            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            monan.FStatus = false;
            db.Entry(monan).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MonAn");
        }

        // edit món
        [HttpGet]
        public ActionResult EditFood(int id)
        {
            Food monan = new Food();
            monan = db.Foods.SingleOrDefault(a => a.FoodID == id);
            monan.listCategory = db.Categories.ToList();
            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(monan);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditFood(Food monan,HttpPostedFileBase fileupload)
        {
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Chọn ảnh món ăn";
                return View();
            }
            else
            {
                 if (ModelState.IsValid)
                {
                    
                    var fileName = Path.GetFileName(fileupload.FileName);

                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                   
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    monan.Fimage = fileName;
                    monan.FStatus = true;
                    db.Entry(monan).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }    
            }
            return RedirectToAction("MonAn");
        }

    }
}