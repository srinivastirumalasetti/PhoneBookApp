using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BusinessDataModels;
using BusinessEntities;
using BusinessServices.Interface;

namespace GoodExample.Controllers
{

    [RoutePrefix("api/Default")]
    public class DefaultController : ApiController
    {

        //// Declared Readonly variable - one time initialization at runtime
        public readonly IPhoneBookService _phoneBook;
        PhoneBookModel pModel = new PhoneBookModel();
        /////// <summary>
        ///// Contructor injection to initialize required services
        ///// </summary>
        public DefaultController(IPhoneBookService phoneBook)
        {
            this._phoneBook = phoneBook;
        }
              

        ///// <summary>
        ///// Method to fetch all records 
        ///// </summary>
        /////         
        public IHttpActionResult Get()
        {
            try
            {
                List<usp_GetPhoneBookDetails_Result> phoneBookist = new List<usp_GetPhoneBookDetails_Result>();

                phoneBookist = _phoneBook.GetAllRecords();

                if (phoneBookist == null)
                {
                    return NotFound();
                }
                return Ok(phoneBookist);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server error");
            }
        }

        [HttpGet]
        /// <summary>
        /// Method to validate contact number
        /// </summary>
        /// <returns>msg</returns>
        ///         
        [Route("{phoneNumber}")]
        public IHttpActionResult Get(string phoneNumber)


        {
            string strValue = _phoneBook.CheckPhoneNumber(phoneNumber);
            return Ok(strValue);
        }

                       
        //[HttpPost]
        ///// <summary>
        ///// Method to add new contact
        ///// </summary>
        ///// <param name="contact"></param>
        ///// <returns>1</returns>
        public IHttpActionResult Post(PhoneBookModel contact)
        {
            try
            {
                if (contact != null)
                {
                    string strValue = _phoneBook.CheckPhoneNumber(contact.PhoneNumber);
                    if (strValue == "0")
                    {
                        var response = _phoneBook.AddContact(contact);
                        return Ok("Contact added successfully");
                    }
                    else
                    {
                        return Ok("Phone number already exists");
                    }                    
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Error in saving data");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Error in Internal Server");
            }
        }

        //[HttpPut]
        ///// <summary>
        ///// Method to add & update phone book
        ///// </summary>
        ///// <param name="bk"></param>
        ///// <returns>1</returns>
        public IHttpActionResult Put(PhoneBookModel bk)
        {            
            try
            {
                if (bk != null)
                {
                    var result = _phoneBook.UpdateBook(bk);
                    string msg = "";
                    if (result == 1)
                        msg = "Contact Updated";
                    else if (result == 2)
                        msg = "Phone number already exists for another contact";
                    else
                        msg = "Invalid Contact";

                    return Ok(msg);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Error in Post Data");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Error in Internal Server");
            }
        }

        [HttpDelete]
        //[HttpPut]
        ///// <summary>
        ///// Method to add & update phone book
        ///// </summary>
        ///// <param name="bk"></param>
        ///// <returns>1</returns>
        public IHttpActionResult DeleteContact(PhoneBookModel bk)
        {
            try
            {
                if (bk != null)
                {
                    var result = _phoneBook.DeleteContact(bk);
                    string msg = "";
                    if (result == 1)
                        msg = "Contact deleted";
                     else
                        msg = "Invalid Contact";

                    return Ok(msg);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Error in Post Data");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Error in Internal Server");
            }
        }

    }
}
