using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OzonShop.Models;
using Shop.DataBase;
using OzonShop.Filters;
using OzonShop.DataAccess;

namespace OzonShop.Controllers
{
    [Culture]
    public class SearchController : Controller
    { 
        public ActionResult ViewProduct(int productId)
        {
            var product = new ProductRepository().Find(productId);
            List<Parameter> param = ParametersRepository.Select(productId);
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
            List<Picture> picture = PictureRepository.Select(productId);
            ViewBag.Pictures = picture;
            ViewBag.Category = new CategoryRepository().Find(product.CategoryId).Name;
            ViewBag.Tags = TagRepository.GetStringTag(productId);
            ViewBag.IsStore = StoreRepository.Find(productId).Quantity > 0 ? true : false;
            var comments = new ProductRepository().SelectCommets(productId);
            ViewBag.UserNames = new UserRepository().SelectNamesByDescription(comments);
            ViewBag.Comments = comments;
            return View(product);
        }

        public ActionResult FullSearch(String SearchString, int idTag = 0, int categoryId = 0)
        {   
            var productModel = new List<ProductModel>();
            var products = new ProductRepository().Select(SearchString, idTag, categoryId).ToList();

            int first = 0;
            int second = 6;
            second = products.Count() - second >= 0 ? second : products.Count(); 
            for(int i = first; i < second; i++)
            {
                productModel.Add(new ProductModel
                {
                    ProductId = products[i].ProductId,
                    Name = products[i].Name,
                    Picture = PictureRepository.Find(products[i].ProductId).PictureUrl,
                    IsStore = StoreRepository.Find(products[i].ProductId).Quantity > 0 ? true : false,
                    Price = products[i].Price,
                });
            }

            var result = new SearchViewModel() { 
                SearchKeyword = idTag != 0 ? null : SearchString, 
                CurrentPage = 0, 
                MaxPages= products.Count / 5,
                PagingSize = 5,
                SortByFieldList = new List<string>(new String[] {"Price", "Name"}),
                PagingSizeList = new List<int>(new int[] {6,10,15,20}),
                SortByField = "Price",
                SearchResult = productModel,
                idCategory = categoryId,
                idTag = idTag
            };           
            return View(result);
        }

        public ActionResult Search(SearchCriteria criteria)
        {
            var productModel = new List<ProductModel>();
            var products = new ProductRepository().Select(criteria.SearchKeyword, criteria.idTag, criteria.idCategory).ToList();
            
            switch (criteria.GetSortByField())
            {
                case SearchCriteria.SearchFieldType.Price:
                    products = products.AsQueryable().OrderBy(q => q.Price).ToList();
                    break;               

                case SearchCriteria.SearchFieldType.Name:
                default:
                    products = products.AsQueryable().OrderBy(q => q.Name).ToList();
                    break;
            }
            int first = (criteria.CurrentPage)*criteria.GetPageSize();
            int second = first + criteria.GetPageSize();
            second = products.Count() - second >= 0 ? second : products.Count(); 
            for (int i = first; i < second; i++)
            {
                productModel.Add(new ProductModel
                {
                    ProductId = products[i].ProductId,
                    Name = products[i].Name,
                    Picture = PictureRepository.Find(products[i].ProductId).PictureUrl,
                    IsStore = StoreRepository.Find(products[i].ProductId).Quantity > 0 ? true : false,
                    Price = products[i].Price,
                });
            }

            var result = new SearchViewModel()
            {
                SearchKeyword = criteria.idTag != 0 ? null : criteria.SearchKeyword,
                MaxPages = products.Count / criteria.GetPageSize(),
                CurrentPage = criteria.CurrentPage,
                PagingSize = criteria.GetPageSize(),
                SortByFieldList = new List<string>(new String[] { "Price", "Name" }),
                PagingSizeList = new List<int>(new int[] { 6, 10, 15, 20 }),
                SortByField = criteria.SortByField,
                SearchResult = productModel,
                idCategory = criteria.idCategory,
                idTag = criteria.idTag
            };
            return View("FullSearch", result);
        }       
    }
}
