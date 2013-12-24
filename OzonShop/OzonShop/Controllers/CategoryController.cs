using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataBase;
using OzonShop.Filters;
using OzonShop.DataAccess;
using OzonShop.Models;

namespace OzonShop.Controllers
{
    [Culture]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly IRepositoryCategory repositoryCategory;

        public CategoryController(IRepositoryCategory repositoryCategory)
        {
            this.repositoryCategory = repositoryCategory;
        }

        //
        // GET: /Category/
        public ActionResult Category(string SearchString)
        {
            var category = repositoryCategory.Select();
            if (!String.IsNullOrEmpty(SearchString))
            {
                category = repositoryCategory.Select(SearchString);
            }
            return View(category);
        }

        //
        // GET: /Category/EditCategory/5
        public ActionResult EditCategory(int id = 0)
        {
            Category category = repositoryCategory.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/EditCategory/5
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                repositoryCategory.Edit(category);
                return RedirectToAction("Category");
            }
            return View(category);
        }

        //
        // GET: /Category/CreateCategory
        public ActionResult CreateCategory()
        {
            return View();
        }

        //
        // POST: /Category/CreateCategory
        [HttpPost]
        public ActionResult CreateCategory(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                Category categoryParent = repositoryCategory.Find(category.ParentId);
                repositoryCategory.Add(new Category { CategoryId = category.CategoryId, Name = category.Name, Parent = null });
                if (categoryParent != null)
                {
                    repositoryCategory.Edit(category.CategoryId, category.ParentId);
                }
                return RedirectToAction("Category");
            }
            return View(category);
        }

        //
        // GET: /Category/DeleteCategory
        [HttpGet, ActionName("DeleteCategory")]
        public ActionResult DeleteCategory(int id)
        {
            repositoryCategory.Delete(id);
            return RedirectToAction("Category");
        }
    }
}
