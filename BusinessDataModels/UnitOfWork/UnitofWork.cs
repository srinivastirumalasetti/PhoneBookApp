using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Common;
using System.Threading.Tasks;
using System.Data;
using System.Threading;

namespace BusinessDataModels.UnitOfWork
{
    public class UnitofWork : IUnityofWorkAsync
    {
        private readonly IDBFactory _dbFactory;
        private PhoneBookDBEntities _dbContext;
        private DbContextTransaction _transaction;

        public PhoneBookDBEntities DBContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); } 
        }

        public UnitofWork(IDBFactory dbfactory)
        {
            this._dbFactory = dbfactory;
            this._dbContext = DBContext;
        }

        public void BeginTransaction(IsolationLevel isolationlevel  = IsolationLevel.Unspecified)
        {
            _transaction = _dbContext.Database.BeginTransaction(isolationlevel);
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch(System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            //catch (System.Data.Entity.Core.EntityCommandCompilationException ex)
            //{
            //    throw ex;
            //}
            //catch (System.Data.Entity.Core.UpdateException ex)
            //{
            //    throw ex;
            //}
            catch (DbUpdateException ex)
            {
                // Retrieve the error message as a list of strings
                var errorMessages = ex;

                // Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, "The validation errors are: ", fullErrorMessage);

                throw new DbUpdateException(exceptionMessage);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error message as a list of strings
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                // Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, "The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationtoken)
        {
            return await _dbContext.SaveChangesAsync(cancellationtoken);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }

}
