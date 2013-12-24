using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using OzonShop.DataAccess;
using Shop.DataBase;

namespace OzonShop.App_Start
{
    public class BindingsModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryUser>().To<UserRepository>();
            Bind<IRepositoryCategory>().To<CategoryRepository>();
            Bind<IRepositoryOrder>().To<OrderRepository>();
            Bind<IRepositoryProduct>().To<ProductRepository>();
        }
    }
}