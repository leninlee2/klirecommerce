using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Klir.TechChallenge.Domain.EntityModel
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Guid IdPromotion { get; set; }

        public bool Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime AlterDate { get; set; }

        public string Url { get; set; }


    }

}
