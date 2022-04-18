using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Klir.TechChallenge.Domain.Interface.Service
{
    public interface IProductService : IService<Product>
    {
        Product findProductById(Guid guid);
    }

}
