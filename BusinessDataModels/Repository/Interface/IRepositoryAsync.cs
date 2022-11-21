using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDataModels
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(long id);
        //Get All Entity

        Task<IEnumerable<TEntity>> GetAllAsync();
        // Marks as entity as Delete

        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        // Marks an entity as delete

        void DeleteAsync(Expression<Func<TEntity, bool>> where);

    }
}
