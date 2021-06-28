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
            return View(listFood);
        }
        // lấy loại món
        public ActionResult CategoriesFood()
        {
            var listCategories = db.Categories.ToList();
            return PartialView(listCategories);
        }
        // lấy món mới nhất
        public ActionResult NewestFood()
        {
            var newFood = db.Foods.OrderByDescending(p => p.FoodID).Take(1);

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

        
    }
}