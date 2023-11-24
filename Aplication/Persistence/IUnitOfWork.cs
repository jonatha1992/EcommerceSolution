using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Aplication.Persistence
{
    public interface IUnitOfWork : IDisposable
    {

        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> Complete();   
    }
}
