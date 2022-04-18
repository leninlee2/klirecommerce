using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<EntityEntry<TEntity>> Add(TEntity entry);
        Task<EntityEntry<TEntity>> Update(TEntity entry);

        IQueryable<TEntity> findAll();

    }

}
