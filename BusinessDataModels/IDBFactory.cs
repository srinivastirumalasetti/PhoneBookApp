using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDataModels
{
    public interface IDBFactory : IDisposable
    {
        PhoneBookDBEntities Init();
    }
}
