using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessDataModels.Repository.Interface;

namespace BusinessDataModels.Repository
{
    public class PhoneBookRepository : RepositoryBase<tblPhoneBookApp>, IPhoneBookRepository
    {
        //private readonly IDbSet<tblUserRole> _dbSet;

        public PhoneBookRepository(IDBFactory dbFactory) : base(dbFactory)
        {
            //_dbSet = DbContext.Set<tblUserRole>();
        }
      
    }
}
