using System;
using System.Collections.Generic;
using System.Text;

namespace Klir.TechChallenge.Domain.Model
{
    public class ShoppingCartRequest
    {
        public string IdProduct { get; set; }
        public int Quantity { get; set; }

        public string Id { get; set; }

        public string IdClient { get; set; }
    }

}
