using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzonShop.Models
{
    public class SearchCriteria
    {
        public enum SearchFieldType
        {
            Price,
            Name
        }

        public string SearchKeyword { get; set; }
        public int idCategory { get; set; }
        public int idTag { get; set; }
        public string SortByField { get; set; }
        public string PagingSize { get; set; }
        public int CurrentPage { get; set; }

        public int GetPageSize()
        {
            int result = 5;
            if (!string.IsNullOrEmpty(this.PagingSize))
            {
                int.TryParse(this.PagingSize, out result);
            }
            return result;
        }

        public SearchFieldType GetSortByField()
        {
            SearchFieldType result;
            switch (SortByField)
            {
                case "Price":
                    result = SearchFieldType.Price;
                    break;
                default:
                    result = SearchFieldType.Name;
                    break;
            }
            return result;
        }
    }
}