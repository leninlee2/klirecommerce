using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Klir.TechChallenge.Domain.EntityModel
{
    public class Promotion
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime AlterDate { get; set; }


    }

}
