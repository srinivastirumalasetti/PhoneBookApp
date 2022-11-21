using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessDataModels;
using BusinessEntities;

namespace BusinessServices.Interface
{
    public interface IPhoneBookService
    {
        List<usp_GetPhoneBookDetails_Result> GetAllRecords();
        int AddContact(PhoneBookModel cModel);
        string CheckPhoneNumber(string phoneNumber);
        int UpdateBook(PhoneBookModel bModel);
        int DeleteContact(PhoneBookModel bModel);

    }
}
