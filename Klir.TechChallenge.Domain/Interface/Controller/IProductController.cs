using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Klir.TechChallenge.Domain.Interface.Controller
{
    public interface IProductController
    {
        IEnumerable<Product> Get();

        Product GetById(Guid guid);
    }
}
