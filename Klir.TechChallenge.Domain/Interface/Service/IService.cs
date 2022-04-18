using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Service
{
    public interface IService<T>
    {
        Task<Guid> Add(T entry);

        Task<Guid> Update(T entry);

        IQueryable<T> findAll();
    }

}
