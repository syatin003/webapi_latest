using Stepan.Common.Repository;
using Stepan.Common.Service;
using Stepan.Repository.Models;
using Stepan.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.WebApi.Models
{
    public class ApiNinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();
            Bind<IProductService>().To<ProductService>();
            Bind<INoteRepository>().To<NoteRepository>();
            Bind<INoteServices>().To<NoteService>();
        }
    }
}