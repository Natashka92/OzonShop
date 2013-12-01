using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OzonShop.Models
{
    public class ParametersModel
    {
        [Required]
        public string Name { get; set; }
        
        public int NameId { get; set; }

        [Required]
        public string Value { get; set; }
        
        public int ValueId { get; set; }

        public int ProductId { get; set; }
    }
}