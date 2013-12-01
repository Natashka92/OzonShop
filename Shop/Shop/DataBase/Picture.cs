using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Picture")]
    public class Picture
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }
        [Required]
        public String PictureUrl { get; set; }

        [Required]
        public int ProductId { get; set; }

        public static List<Picture> Select(int productId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Pictures.Where(s => s.ProductId == productId).ToList();
            }
        }

        public static void Delete(int id)
        {
            using (var dbContext = new DataContext())
            {
                Picture pic = dbContext.Pictures.Find(id);

                var product = dbContext.Products.Find(pic.ProductId);
                product.Picture.Remove(pic);
                dbContext.Pictures.Remove(pic);
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static void Add(Picture picture)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Pictures.Add(picture);
                var product = dbContext.Products.Find(picture.ProductId);
                List<Picture> listPicture = product.Picture.ToList();
                listPicture.Add(picture);
                product.Picture = listPicture;
                dbContext.Entry(product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static Picture Find(int productId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Pictures.FirstOrDefault(s => s.ProductId == productId);
            }
        }
    }
}
