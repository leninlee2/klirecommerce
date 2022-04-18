using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Klir.TechChallenge.Domain.EntityModel
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }

        public bool Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? AlterDate { get; set; }


    }

}
