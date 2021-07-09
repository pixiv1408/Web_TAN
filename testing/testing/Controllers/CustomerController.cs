using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testing.Models;
using testing.ViewModels;

namespace testing.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        Database db = new Database();
        DangKy dk = new DangKy();

        public ActionResult Index()
        {
           
            return View();
        }
        // Đăng ký tài khoản
        [HttpGet]
        public ActionResult SignUP()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult SignUp(DangKy model)
        {          
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(s => s.Username == model.Username);
                if(check==null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    Customer cus = new Customer();
                    cus.Fullname = model.Fullname;
                    cus.Phone = model.Phone;
                    cus.Address = model.Address;
                    cus.Gmail = model.Gmail;
                    db.Customers.Add(cus);
                    db.SaveChanges();

                    int idCus = cus.CusID;
                    Account acc = new Account();
                    acc.Username = model.Username;
                    acc.Password = model.Password;
                    acc.CusID = idCus;
                    acc.Position = 2;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                   
                    return RedirectToAction("Index", "FoodHome");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập đã tồn tại";
                    return View();
                }
            }
            else
            {
                return View();
            }         
        }

        // Đăng nhập khách
        [HttpGet]
        public ActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection form)
        {
            var username = form["inputuser"];
            var password = form["inputpass"];

            if (ModelState.IsValid)
            {
                Account acc = db.Accounts.SingleOrDefault(a => a.Username == username && a.Password == password);
                if(acc != null)
                {
                    Session["LogIn"] = acc;                
                    Session["FullName"] = acc.Customer.Fullname.ToString();
                    return RedirectToAction("Index", "FoodHome");
                }
                else
                {
                    ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng";                 
                }
            }
            return View();
        }
        // thay đổi mật khẩu
        [HttpGet]
        public ActionResult ChangesPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangesPassword(ChangesPassword obj)
        {
            Account acc = (Account)Session["LogIn"];
            acc = db.Accounts.SingleOrDefault(a => a.CusID == acc.CusID);

            if (ModelState.IsValid)
            {
                if (acc.Password == obj.oldPassword)
                {
                    acc.Password = obj.newPassword2;
                    db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Session.Clear();
                    return RedirectToAction("DangNhap", "Customer");
                }
                else
                {
                    ViewBag.Message = ("Thông tin không chính xác");
                }
            }
            return View();
        }

        // đăng xuất
        public ActionResult logOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "FoodHome");
        }
        
    }
}