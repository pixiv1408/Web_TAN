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
            if(Session["LogIn"] == null || Session["LogIn"].ToString() == "" && Session["Role"].ToString() !="1")
            {
                return RedirectToAction("Index", "FoodHome");
            }
            return View();
        }

        public ActionResult MonAn(int ?page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 8;
            //var listFood = db.Foods.ToList();
            //Food ds = new Food();

            return View(db.Foods.ToList().Where(a=>a.FStatus==true).OrderBy(x=>x.FoodID).ToPagedList(pageNum,pageSize));
        }

        [HttpGet]
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

        // edit
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