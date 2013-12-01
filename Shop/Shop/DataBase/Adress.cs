using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Adress")]
    public class Adress
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AdressId { get; set; }

        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(10)]
        public String Country { get; set; }

        [Required]
        [MaxLength(10)]
        public String Town { get; set; }

        [Required]
        [MaxLength(15)]
        public String Street { get; set; }

        public static List<Adress> Select(int userId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Adresses.Where(s => s.UserId == userId).ToList();
            }
        }

        public static Adress Find(int id)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Adresses.Find(id);
            }
        }
    }
}
