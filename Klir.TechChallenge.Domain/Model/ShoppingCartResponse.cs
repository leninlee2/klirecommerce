using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Klir.TechChallenge.Domain.Model
{
    public class ShoppingCartResponse
    {
        public ShoppingCart shoppingCart { get; set; }

        public Product product { get; set; }

        public Promotion promotion { get; set; }
    }
}
