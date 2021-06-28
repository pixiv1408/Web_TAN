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
        


        [HttpGet]
        public ActionResult _PartialLogin()
        {
            
            return PartialView();
        }
        
        
    }
}