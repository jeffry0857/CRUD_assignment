using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.GetAll();
            return View(objCategoryList);
        }
        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            // 如果輸入的類別名稱跟Display Order一樣則會跳出警示
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "類別名稱不能跟Display Order一樣");
            }
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        // GET
        public IActionResult Edit(int? id)
        {
            // 如果id等於null 或 等於 0, 回傳找不到
            if (id== null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _db.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            // 如果輸入的類別名稱跟Display Order一樣則會跳出警示
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "類別名稱不能跟Display Order一樣");
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            // 如果id等於null 或 等於 0, 回傳找不到
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _db.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();
            return RedirectToAction("Index");
        }
    }
}
