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
    public class QLMonAnController : Controller
    {
        Database db = new Database();
        // GET: QLMonAn
        public ActionResult Index()
        {
            return View();
        }

        // show list món ăn
        public ActionResult MonAn(int? page)
        {
            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pageNum = (page ?? 1);
                int pageSize = 7;
                return View(db.Foods.ToList().Where(a => a.FStatus == true && a.Category.CStatus == true).OrderBy(x => x.FoodID).ToPagedList(pageNum, pageSize));
            }
        }

        //Thêm món
        public ActionResult CreateNew()
        {
            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                Food mon = new Food();
                mon.listCategory = db.Categories.Where(a => a.CStatus == true).ToList();
                return View(mon);
            }
        }
        [HttpPost]
        public ActionResult CreateNew(Food monan, HttpPostedFileBase fileupload)
        {
            if (fileupload == null)
            {
                ViewBag.ThongBaoHinh = "Chọn ảnh món ăn";
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


            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                Food monan = db.Foods.SingleOrDefault(a => a.FoodID == id);
                return View(monan);
            }

        }
        // xóa món
        public ActionResult DeleteF(int id)
        {

            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                Food monan = db.Foods.SingleOrDefault(a => a.FoodID == id);
                return View(monan);
            }

        }
        [HttpPost, ActionName("DeleteF")]
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
            if (Session["LoginAD"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                Food monan = new Food();
                monan = db.Foods.SingleOrDefault(a => a.FoodID == id);
                monan.listCategory = db.Categories.Where(a => a.CStatus == true).ToList();
                return View(monan);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditFood(Food monan, HttpPostedFileBase fileupload)
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