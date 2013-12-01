using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzonShop.Models
{
    public class SearchViewModel
    {
        public string SearchKeyword { get; set; }
        public int idCategory { get; set; }
        public int idTag { get; set; }

        public int CurrentPage { get; set; }
        public int MaxPages { get; set; }

        public int PagingSize { get; set; }
        public IEnumerable<int> PagingSizeList { get; set; }

        public string SortByField { get; set; }
        public IEnumerable<string> SortByFieldList { get; set; }

        public IEnumerable<ProductModel> SearchResult { get; set; }
    }
}