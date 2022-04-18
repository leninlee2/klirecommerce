using System;
using System.Configuration;
using Klir.TechChallenge.Domain.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Klir.TechChallenge.InfraStructure.ContextModel
{
    public partial class KlirContext : DbContext
    {
        public KlirContext()
        {
        }

        public KlirContext(DbContextOptions<KlirContext> options)
            : base(options)
        {
        }

        

        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<GroupShoopingCart> GroupShoopingCart { get; set; }

    }
}



