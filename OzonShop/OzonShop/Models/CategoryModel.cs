using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OzonShop.Models
{
    public class CategoryModel
    {
        [Required]        
        public int CategoryId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public int  ParentId { get; set; }
    }
}
