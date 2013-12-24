using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Store")]
    public class Store
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }        
    }
}
