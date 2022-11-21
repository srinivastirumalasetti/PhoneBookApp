using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessDataModels;
using BusinessDataModels.Repository.Interface;
using BusinessDataModels.UnitOfWork;
using BusinessEntities;
using BusinessServices.Interface;

namespace BusinessServices
{
    public class PhoneBookService : IPhoneBookService
    {
        // Declared Readonly variable - one time initialization at runtime

        public readonly IPhoneBookRepository _phoneBookRepository;
        public readonly IUnitofWork _unitOfWork;

        ///// <summary>
        /// Contructor injection to initialize required repository services
        /// </summary>
        public PhoneBookService(IPhoneBookRepository phoneBookRepository, IUnitofWork unitOfWork)
        {
            this._phoneBookRepository = phoneBookRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method to fetch all records 
        /// </summary>
        ///   
        public List<usp_GetPhoneBookDetails_Result> GetAllRecords()
        {
            List<usp_GetPhoneBookDetails_Result> listdata;
            listdata = _unitOfWork.DBContext.usp_GetPhoneBookDetails().ToList();
            return listdata;
        }

        
        /// <summary>
        /// Method to add contact
        /// </summary>
        /// <returns>1</returns>
        ///   
        public int AddContact(PhoneBookModel cModel)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                PhoneBookModel ctModel = new PhoneBookModel();
                ctModel.FirstName = cModel.FirstName;
                ctModel.LastName = cModel.LastName;
                ctModel.PhoneNumber = cModel.PhoneNumber;

                _phoneBookRepository.Add(Mapper.Map<tblPhoneBookApp>(ctModel));
                
                SaveContact();
                _unitOfWork.Commit();

                return 1;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Method to get validate contact details
        /// </summary>
        /// <returns>1</returns>
        ///  
        public string CheckPhoneNumber(string phoneNumber)
        {
            string strValue = "0";
            PhoneBookModel contactDetails = Mapper.Map<PhoneBookModel>(_phoneBookRepository.Get(x => x.phonenumber == phoneNumber));

            if (contactDetails != null)
            {
                strValue = "1";
            }
            return strValue;
        }

        /// <summary>
        /// Method to update phone book 
        /// </summary>
        /// <returns>1</returns>
        ///  
        public int UpdateBook(PhoneBookModel bModel)
        {
            try
            {
                int res = 1;

                PhoneBookModel contactDetails = Mapper.Map<PhoneBookModel>(_phoneBookRepository.Get(x => x.phonenumber == bModel.PhoneNumber && x.phoneID != bModel.PhoneID));

                if (contactDetails != null)
                {
                    res = 2;
                }
                else
                {
                    _unitOfWork.BeginTransaction();

                    tblPhoneBookApp btModel = _phoneBookRepository.GetById(bModel.PhoneID);

                    _phoneBookRepository.Detach(btModel);

                    btModel.phoneID = bModel.PhoneID;
                    btModel.firstname = bModel.FirstName;
                    btModel.lastname = bModel.LastName;
                    btModel.phonenumber = bModel.PhoneNumber;

                    _phoneBookRepository.Update(btModel);

                    SaveContact();
                    _unitOfWork.Commit();
                }

                return res;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Method to delete contact details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteContact(PhoneBookModel bModel)
        {
            PhoneBookDBEntities obj = new PhoneBookDBEntities();
            var contactList = obj.tblPhoneBookApps.Where(x => x.phoneID == bModel.PhoneID).FirstOrDefault();
            obj.tblPhoneBookApps.Remove(contactList);
            int res = obj.SaveChanges();
            return res;
        }

        /// <summary>
        /// Method to save unit of work changes
        /// </summary>
        /// <returns>1</returns>
        ///  
        public void SaveContact()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
