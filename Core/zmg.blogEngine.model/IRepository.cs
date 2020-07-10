using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zmg.blogEngine.model
{
    public interface IRepository
    {
        Task<Guid> Save(object obj);

        void SaveOrUpdateAsync(object obj);

        void DeleteAsync(object obj);

        Task<object> GetById(Type objType, object objId);

        Task<object> GetByIdReadOnly(Type objType, object objId);

        Task EvictAsync(object obj);

        IQueryable<TEntity> ToList<TEntity>();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
