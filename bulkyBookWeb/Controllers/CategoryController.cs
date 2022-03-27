using bulkyBookWeb.Data;
using bulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulkyBookWeb.Controllers
{
    public class CategoryController : Controller

  
    {

        
        private readonly ApplicationDbContext _dB;

        public CategoryController (ApplicationDbContext db)
        {
            _dB = db;
        }
        public IActionResult Index()
        {
            IEnumerable <Category> objCategoryList = _dB.Categories;
            return View(objCategoryList);
        }

        //Get
        public IActionResult Create()
        {


            return View();
            
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Category obj)
        
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot Extracly mach the Name");
            }

            if (ModelState.IsValid) 
            { 

            _dB.Categories.Add(obj);
            _dB.SaveChanges();
            TempData["Success"] = "Category Created Successfully";
            return RedirectToAction("Index");

            }

            return View(obj);

        }

        //Get
        public IActionResult Edit(int? id)
        {

            if (id==null || id==0)

            {
                return NotFound();
            }

            var categoryFromDb = _dB.Categories.Find(id);

            //var categoryFromDBFirst = _dB.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDBSingle = _dB.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb == null)

            {
                return NotFound();
            }
            return View(categoryFromDb);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Category obj)

        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot Extracly mach the Name");
            }

            if (ModelState.IsValid)
            {

                _dB.Categories.Update(obj);
                _dB.SaveChanges();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");

            }

            return View(obj);

        }

        //Get
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)

            {
                return NotFound();
            }

            var categoryFromDb = _dB.Categories.Find(id);

            //var categoryFromDBFirst = _dB.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDBSingle = _dB.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb == null)

            {
                return NotFound();
            }
            return View(categoryFromDb);

        }

        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)

        {

            var obj = _dB.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            

                _dB.Categories.Remove(obj);
                _dB.SaveChanges();
                TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

            

            

        }

    }
}
