using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.InfraStructure.ContextModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.InfraStructure.Repository.EntityRepository
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly KlirContext Context;
        protected readonly DbSet<TEntity> Set;

        public EntityRepository(KlirContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        public async Task<EntityEntry<TEntity>> Add(TEntity entity)
        {
            EntityEntry<TEntity> entry = Set.Add(entity);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entry;
        }

        public IQueryable<TEntity> findAll()
        {
            return Set;
        }

        public async Task<EntityEntry<TEntity>> Update(TEntity entity)
        {
            EntityEntry<TEntity> entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
                entry = Set.Update(entity);
            else if (entry.State == EntityState.Added)
                entry = Set.Add(entity);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return entry;
        }

       
    }

}
