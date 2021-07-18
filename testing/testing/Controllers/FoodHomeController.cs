using System.Linq;
using System.Web.Mvc;
using testing.Models;

namespace testing.Controllers
{
    public class FoodHomeController : Controller
    {
        // GET: FoodHome
        Database db = new Database();
        // lấy food
        public ActionResult Index()
        {
            var listFood = db.Foods.ToList();
            return View(listFood.OrderBy(a=>a.CID).Where(x=>x.FStatus==true && x.Category.CStatus==true));
        }
        // lấy loại món
        public ActionResult CategoriesFood()
        {           
            //var listCategories = db.Categories.ToList();
            var cate = (from p in db.Categories join a in db.Foods on p.CateID equals a.CID where p.CStatus == true select p).Distinct();
            return PartialView(cate);
        }
        // lấy món mới nhất
        public ActionResult NewestFood()
        {
            var newFood = db.Foods.OrderByDescending(p => p.FoodID).Take(1).Where(a=>a.FStatus==true);        
            return PartialView(newFood);
        }
        //lấy món theo loại
        public ActionResult FoodByCate(int id)
        {
            var result = from p in db.Foods where p.CID == id select p;
            return View(result);
        }
        // lấy chi tiết món
        public ActionResult Details(int? id)
        {
            var foodDetail = from s in db.Foods
                             where s.FoodID == id
                             select s;
            
            return View(foodDetail.SingleOrDefault());
        }

        //tìm kiếm món ăn
        public ActionResult SearchFood(string searchStr)
        {        
            var search = from fo in db.Foods where fo.FStatus==true  select fo;
            if (!string.IsNullOrEmpty(searchStr))
            {
                search = search.Where(s => s.FName.Contains(searchStr.ToLower()));
            }
            return View(search);
        }    
    }
}