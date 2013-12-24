using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Tag")]
    public class Tag : INamedEntity
    {
        public Tag()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }

        [Required]
        [MaxLength(20)]
        public String Name { get; set; }

        [Required]
        public int Value { get; set; }

        public virtual ICollection<Product> Products { get; set; }       
    }

    public class TagCloud
    {
        public TagCloud() { }

        public TagCloud(String name, String css, int total)
        {
            this.Name = name;
            this.CssClass = css;
            this.Total = total;
        }

        public TagCloud(int id, String name, String css, int total)
        {
            this.Id = id;
            this.Name = name;
            this.CssClass = css;
            this.Total = total;
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public String CssClass { get; set; }
        public int Total { get; set; }
    }
}
