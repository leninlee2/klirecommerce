using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Klir.TechChallenge.Domain.EntityModel
{
    public class GroupShoopingCart
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdClient { get; set; }

        public bool Status { get; set; }

        public bool? Close { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? AlterDate { get; set; }
    }

}
