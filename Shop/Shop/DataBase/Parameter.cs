using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.DataBase
{
    [Table("Parameter")]
    public class Parameter
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ParameterId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int ParamNameId { get; set; }

        [Required]
        public int ParamValueId { get; set; }

        public static List<Parameter> Select(int productId)
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Parameters.Where(s => s.ProductId == productId).ToList();
            }
        }

        public static String FindName(int id)
        {
            using (var dbContext = new DataContext())
            {
                var param = dbContext.ParamNames.Find(id);
                if (param != null)
                    return param.Name.ToString();
                return "null";
            }
        }

        public static String FindValue(int id)
        {
            using (var dbContext = new DataContext())
            {
                var param = dbContext.ParamValues.Find(id);
                if (param != null)
                    return param.Name.ToString();
                return "null";
            }
        }

        public static void Add(int productId, string name, string value)
        {
            using (var dbContext = new DataContext())
            {
                int nameId = 0;
                int valueId = 0;
                var paramName = dbContext.ParamNames.FirstOrDefault(s => s.Name.Equals(name));
                var paramValue = dbContext.ParamValues.FirstOrDefault(s => s.Name.Equals(value));
                if (paramName == null)
                {
                    paramName = dbContext.ParamNames.Add(new ParamName() { Name = name });
                    dbContext.SaveChanges();
                }
                nameId = paramName.ParamNameId;

                if (paramValue == null)
                {
                    paramValue = dbContext.ParamValues.Add(new ParamValue() { Name = value });
                    dbContext.SaveChanges();
                }
                valueId = paramValue.ParamValueId;

                dbContext.Parameters.Add(new Parameter()
                {
                    ParamValueId = valueId,
                    ParamNameId = nameId,
                    ProductId = productId
                });
                dbContext.SaveChanges();
            }
        }

        public static void Delete(int idProduct, int idValue, int idName)
        {
            using (var dbContext = new DataContext())
            {
                var param = dbContext.Parameters
                    .Where(s => s.ParamNameId == idName)
                    .Where(s => s.ParamValueId == idValue)
                    .First(s => s.ProductId == idProduct);
                if (param != null)
                {
                    dbContext.Parameters.Remove(param);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
