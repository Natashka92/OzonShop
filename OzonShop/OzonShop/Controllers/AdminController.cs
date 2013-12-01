using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzonShop.Filters;
using Shop;
using Shop.DataBase;
using OzonShop.Models;

namespace OzonShop.Controllers
{
    [Culture]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Product/
        public ActionResult Product(string SearchString)
        {
            var products = Shop.DataBase.Product.Select();
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = Shop.DataBase.Product.Select(SearchString, 0, 0);
            }
            return View(products);
        }

        //
        // GET: /Category/
        public ActionResult Category(string SearchString)
        {
            var category = Shop.DataBase.Category.Select();
            if (!String.IsNullOrEmpty(SearchString))
            {
                category = Shop.DataBase.Category.Select(SearchString);
            }
            return View(category);
        }

        //
        // GET: /Order/
        public ActionResult Order()
        {
            var order = Shop.DataBase.Order.Select();
            return View(order);
        }

        //
        // GET: /User/
        public ActionResult ViewUser(string SearchString)
        {
            var users = Shop.DataBase.User.Select();
            if (!String.IsNullOrEmpty(SearchString))
            {
                users = Shop.DataBase.User.Select(SearchString);
            }
            return View(users);
        }

        //
        // GET: /Admin/DetailsUser/5
        public ActionResult DetailsUser(int id)
        {
            User user = Shop.DataBase.User.Find(id);
            ViewBag.Adress = Shop.DataBase.Adress.Select(id);
            return View(user);
        }

        //
        // GET: /Admin/DetailsOrder/5
        public ActionResult DetailsOrder(int id)
        {
            Order order = Shop.DataBase.Order.Find(id);
            var adress = Adress.Find(order.AdressId);
            ViewBag.Adress = adress.Country + " " + adress.Town + " " + adress.Street;
            ViewBag.Currency = Currency.Find(order.CurrencyId).Name;
            ViewBag.User = Shop.DataBase.User.Find(order.UserId).UserName;
            ViewBag.Products = Shop.DataBase.Order.SelectOrderProduct(id);
            return View(order);
        }

        //
        // GET: /Admin/DetailsProduct/5
        public ActionResult DetailsProduct(int id)
        {
            Product product = Shop.DataBase.Product.Find(id);
            List<Parameter> param = Parameter.Select(id);
            List<ParametersModel> paramModel = new List<ParametersModel>();
            foreach(var item in param)
            {
                ParametersModel model = new ParametersModel();
                model.NameId = item.ParamNameId;
                model.ValueId = item.ParamValueId;
                model.Name = Parameter.FindName(item.ParamNameId);
                model.Value = Parameter.FindValue(item.ParamValueId);
                paramModel.Add(model);
            }

            ViewBag.Param = paramModel;
            List<Picture> picture = Picture.Select(id);
            ViewBag.Pictures = picture;
            ViewBag.Category = Shop.DataBase.Category.Find(product.CategoryId).Name;
            return View(product);
        }

        //
        // GET: /Admin/EditCategory/5
        public ActionResult EditCategory(int id = 0)
        {
            Category category = Shop.DataBase.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Admin/EditCategory/5
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                Shop.DataBase.Category.Edit(category);
                return RedirectToAction("Category");
            }
            return View(category);
        }
                
        //
        // GET: /Admin/EditProduct/5
        public ActionResult EditProduct(int id = 0)
        {
            Product product = Shop.DataBase.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            List<Parameter> param = Parameter.Select(id);
            List<ParametersModel> paramModel = new List<ParametersModel>();
            foreach (var item in param)
            {
                ParametersModel model = new ParametersModel();
                model.NameId = item.ParamNameId;
                model.ValueId = item.ParamValueId;
                model.Name = Parameter.FindName(item.ParamNameId);
                model.Value = Parameter.FindValue(item.ParamValueId);
                paramModel.Add(model);
            }

            ViewBag.Param = paramModel;
            List<Picture> picture = Picture.Select(id);
            ViewBag.Pictures = picture;
            ViewBag.Categories = Shop.DataBase.Category.Select().ToArray();
            return View(product);
        }

        //
        // POST: /Admin/EditProduct/5
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                Shop.DataBase.Product.Edit(product);
                return RedirectToAction("Product");
            }
            return View(product);
        }
               
        //
        // GET: /Admin/CreateCategory
        public ActionResult CreateCategory()
        {
            return View();
        }

        //
        // POST: /Admin/CreateCategory
        [HttpPost]
        public ActionResult CreateCategory(CategoryModel category)
        {           
            if (ModelState.IsValid)
            {
                Category categoryParent = Shop.DataBase.Category.Find(category.ParentId);
                Shop.DataBase.Category.Add(new Category { CategoryId = category.CategoryId, Name = category.Name, Parent = null });
                if (categoryParent != null)
                {
                    Shop.DataBase.Category.Edit(category.CategoryId, category.ParentId);
                }
                return RedirectToAction("Category");
            }
            return View(category);
        }

        //
        // GET: /Admin/CreateParameter
        public ActionResult CreateParameter(int productId)
        {
            return View(new ParametersModel() { ProductId = productId });
        }

        //
        // POST: /Admin/CreateParameter
        [HttpPost]
        public ActionResult CreateParameter(ParametersModel param)
        {
            if (ModelState.IsValid)
            {
                Parameter.Add(param.ProductId, param.Name, param.Value);
                return RedirectToAction("EditProduct", new { id = param.ProductId });
            }
            return View(param);
        }
                
        //
        //GET: /Admin/CreateProduct
        public ActionResult CreateProduct()
        {
            ViewBag.Category = Shop.DataBase.Category.Select().ToArray();
            ViewBag.ListTag = Tag.Select();
            return View();
        }

        //
        //POST: /Admin/CreateProduct
        [HttpPost]
        public ActionResult CreateProduct(Product product, String StringTags)
        {
            if (ModelState.IsValid)
            {
                Shop.DataBase.Product.Add(product, StringTags);
                return RedirectToAction("Product");
            }
            ViewBag.Category = Shop.DataBase.Category.Select().ToArray();
            return View(product);
        }

        //
        // GET: /Admin/CreatePicture
        public ActionResult CreatePicture(int idProduct)
        {
            return View(new Picture() { ProductId = idProduct });
        }

        //
        // POST: /Admin/CreatePicture
        [HttpPost]
        public ActionResult CreatePicture(Picture picture)
        {
            if (ModelState.IsValid)
            {
                Picture.Add(picture);
                return RedirectToAction("EditProduct", new { id = picture.ProductId });
            }
            return View(picture);
        }

        //
        // GET: /Admin/DeleteProduct
        [HttpGet, ActionName("DeleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            Shop.DataBase.Product.Delete(id);
            return RedirectToAction("Product");
        }

        //
        // GET: /Admin/DeleteOrder
        [HttpGet, ActionName("DeleteOrder")]
        public ActionResult DeleteOrder(int id)
        {
            Shop.DataBase.Order.Delete(id);
            return RedirectToAction("Order");
        }

        //
        // GET: /Admin/DeleteCategory
        [HttpGet, ActionName("DeleteCategory")]
        public ActionResult DeleteCategory(int id)
        {
            Shop.DataBase.Category.Delete(id);
            return RedirectToAction("Category");
        }

        //
        // GET: /Admin/DeleteUser
        [HttpGet, ActionName("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            Shop.DataBase.User.Delete(id);
            return RedirectToAction("ViewUser");
        }

        // GET: /Admin/DeletePicture
        [HttpGet, ActionName("DeletePicture")]
        public ActionResult DeletePicture(int pictureId, int productId)
        {
            Picture.Delete(pictureId);
            return RedirectToAction("EditProduct", new { id = productId });
        }

        // GET: /Admin/DeleteParameter
        [HttpGet, ActionName("DeleteParameter")]
        public ActionResult DeleteParameter(int idName, int idValue, int productId)
        {
            Parameter.Delete(productId, idValue, idName);
            return RedirectToAction("EditProduct", new { id = productId });
        }
    }
}
