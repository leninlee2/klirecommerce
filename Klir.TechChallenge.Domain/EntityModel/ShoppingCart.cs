using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Klir.TechChallenge.Domain.EntityModel
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdProduct { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double Total { get; set; }

        public Guid IdPromotion { get; set; }

        public Guid? IdGroupShoppingCart { get; set; }

        public bool Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime AlterDate { get; set; }

    }

}
