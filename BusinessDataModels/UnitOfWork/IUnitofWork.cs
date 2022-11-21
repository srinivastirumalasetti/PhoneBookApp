using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDataModels.UnitOfWork
{
    public interface IUnitofWork
    {
        PhoneBookDBEntities DBContext { get;}

        void BeginTransaction(IsolationLevel isolationlevel = IsolationLevel.Unspecified);

        void SaveChanges();

        void Rollback();

        bool Commit();
    }
}
