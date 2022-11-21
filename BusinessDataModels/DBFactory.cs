using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDataModels
{
    public class DBFactory : Disposable,IDBFactory
    {
        PhoneBookDBEntities dbContext;

        public PhoneBookDBEntities Init()
        {
            return dbContext ?? (dbContext = new PhoneBookDBEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
