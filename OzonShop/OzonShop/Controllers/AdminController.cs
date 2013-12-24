using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzonShop.Filters;
using Shop;
using Shop.DataBase;
using OzonShop.Models;
using OzonShop.DataAccess;

namespace OzonShop.Controllers
{    
    [Culture]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IRepositoryProduct repositoryProduct;

        public AdminController(IRepositoryProduct repositoryProduct)
        {
            this.repositoryProduct = repositoryProduct;
        }

        //
        // GET: /Admin//Product/
        public ActionResult Product(string SearchString)
        {
            var products = repositoryProduct.Select();
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = repositoryProduct.Select(SearchString, 0, 0);
            }
            return View(products);
        }

        //
        // GET: /Admin/DetailsProduct/5
        public ActionResult DetailsProduct(int id)
        {
            Product product = repositoryProduct.Find(id);
            List<Parameter> param = ParametersRepository.Select(id);
            List<ParametersModel> paramModel = new List<ParametersModel>();
            foreach(var item in param)
            {
                ParametersModel model = new ParametersModel();
                model.NameId = item.ParamNameId;
                model.ValueId = item.ParamValueId;
                model.Name = ParametersRepository.FindName(item.ParamNameId);
                model.Value = ParametersRepository.FindValue(item.ParamValueId);
                paramModel.Add(model);
            }

            ViewBag.Param = paramModel;
            List<Picture> picture = PictureRepository.Select(id);
            ViewBag.Pictures = picture;
            ViewBag.Category = new CategoryRepository().Find(product.CategoryId).Name;
            return View(product);
        }
       
        //
        // GET: /Admin/EditProduct/5
        public ActionResult EditProduct(int id = 0)
        {
            Product product = repositoryProduct.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            List<Parameter> param = ParametersRepository.Select(id);
            List<ParametersModel> paramModel = new List<ParametersModel>();
            foreach (var item in param)
            {
                ParametersModel model = new ParametersModel();
                model.NameId = item.ParamNameId;
                model.ValueId = item.ParamValueId;
                model.Name = ParametersRepository.FindName(item.ParamNameId);
                model.Value = ParametersRepository.FindValue(item.ParamValueId);
                paramModel.Add(model);
            }

            ViewBag.Param = paramModel;
            List<Picture> picture = PictureRepository.Select(id);
            ViewBag.Pictures = picture;
            ViewBag.Categories = new CategoryRepository().Select().ToArray();
            return View(product);
        }

        //
        // POST: /Admin/EditProduct/5
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                repositoryProduct.Edit(product);
                return RedirectToAction("Product");
            }
            return View(product);
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
                ParametersRepository.Add(param.ProductId, param.Name, param.Value);
                return RedirectToAction("EditProduct", new { id = param.ProductId });
            }
            return View(param);
        }
                
        //
        //GET: /Admin/CreateProduct
        public ActionResult CreateProduct()
        {
            ViewBag.Category = new CategoryRepository().Select().ToArray();
            ViewBag.ListTag = TagRepository.Select();
            return View();
        }

        //
        //POST: /Admin/CreateProduct
        [HttpPost]
        public ActionResult CreateProduct(Product product, String StringTags)
        {
            if (ModelState.IsValid)
            {
                repositoryProduct.Add(product, StringTags);
                return RedirectToAction("Product");
            }
            ViewBag.Category = new CategoryRepository().Select().ToArray();
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
                PictureRepository.Add(picture);
                return RedirectToAction("EditProduct", new { id = picture.ProductId });
            }
            return View(picture);
        }

        //
        // GET: /Admin/DeleteProduct
        [HttpGet, ActionName("DeleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            repositoryProduct.Delete(id);
            return RedirectToAction("Product");
        }
 
        // GET: /Admin/DeletePicture
        [HttpGet, ActionName("DeletePicture")]
        public ActionResult DeletePicture(int pictureId, int productId)
        {
            PictureRepository.Delete(pictureId);
            return RedirectToAction("EditProduct", new { id = productId });
        }

        // GET: /Admin/DeleteParameter
        [HttpGet, ActionName("DeleteParameter")]
        public ActionResult DeleteParameter(int idName, int idValue, int productId)
        {
            ParametersRepository.Delete(productId, idValue, idName);
            return RedirectToAction("EditProduct", new { id = productId });
        }
    }
}
