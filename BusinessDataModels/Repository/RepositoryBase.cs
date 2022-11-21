using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessDataModels
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        private PhoneBookDBEntities _datacontext;
        private readonly IDbSet<TEntity> _dbset;

        protected IDBFactory DbFactory
        {
            get;
            private set;
        }

        protected PhoneBookDBEntities DbContext
        {
            get { return _datacontext ?? (_datacontext = DbFactory.Init()); }
        }

        protected RepositoryBase(IDBFactory dbFactory)
        {
           DbFactory = dbFactory;
            _dbset = DbContext.Set<TEntity>();

        }
        public virtual void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _datacontext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Detach(TEntity entity)
        {
            DbContext.Entry(entity).State = System.Data.Entity.EntityState.Detached;            
        }

        public virtual void Delete(TEntity entity)
        {
            _dbset.Remove(entity);            
        }
        
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = _dbset.Where<TEntity>(where).AsEnumerable();
            DbContext.Set<TEntity>().RemoveRange(objects);
        }

        public virtual TEntity GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbset.ToList();
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _dbset.ToListAsync();
            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var result = await _dbset.Where<TEntity>(where).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async void DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = await _dbset.Where<TEntity>(where).ToListAsync();
            DbContext.Set<TEntity>().RemoveRange(objects);
        }

        public async virtual Task<TEntity> GetByIdAsync(long id)
        {
            var result = await DbContext.Set<TEntity>().FindAsync(id);
            return result;
        }

        public virtual IEnumerable<TEntity> GeWithSqlQuery(string query, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TEntity>(query, parameters).ToList();
        }

    }
}
