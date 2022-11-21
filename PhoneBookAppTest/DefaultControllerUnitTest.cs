using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using BusinessDataModels;
using BusinessDataModels.Repository;
using BusinessDataModels.UnitOfWork;
using BusinessEntities;
using BusinessServices;
using BusinessServices.AutoMapperMapping;
using GoodExample.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhoneBookAppTest
{
    [TestClass]
    public class DefaultControllerUnitTest
    {
        DefaultController defaultController = new DefaultController(new PhoneBookService(new PhoneBookRepository(new DBFactory()),
             new UnitofWork(new DBFactory())));

        public DefaultControllerUnitTest()
        {
            Mapper.Initialize(x => { x.AddProfile<MappingProfiles>(); });
        }

        [TestMethod]
        public void GetContacts()
        {
            defaultController.Configuration = new HttpConfiguration();
            IHttpActionResult actionResult = defaultController.Get();

            // Assert
            var FoundRes = actionResult as OkNegotiatedContentResult<List<usp_GetPhoneBookDetails_Result>>;

            // Assert                        
            Assert.IsNotNull(actionResult);
            
        }

        [TestMethod]
        public void GetcheckPhoneNumber()
        {
            defaultController.Configuration = new HttpConfiguration();
            IHttpActionResult actionResult1 = defaultController.Get("1234567"); // Pass dummy value not available in DB
            IHttpActionResult actionResult2 = defaultController.Get("88677567891"); // Pass available value in DB

            // Assert
            var FoundRes1 = actionResult1 as OkNegotiatedContentResult<string>;
            var FoundRes2 = actionResult2 as OkNegotiatedContentResult<string>;

            // Assert                        
            Assert.IsNotNull(actionResult1);
            Assert.AreEqual("0", Convert.ToString(FoundRes1.Content));
            Assert.AreEqual("1", Convert.ToString(FoundRes2.Content));
        }

        [TestMethod]
        // post correct record 
        public void PostCorrectRecord()
        {               
            defaultController.Configuration = new HttpConfiguration();

            PhoneBookModel bModel = new PhoneBookModel
            {
                PhoneID = 0,
                FirstName = "Srinivas",
                LastName = "Tirumalasetti",
                PhoneNumber = "760589242123"                
            };

            IHttpActionResult actionResult = defaultController.Post(bModel);
            var FoundRes = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Contact added successfully", FoundRes.Content);
        }

        [TestMethod]
        // post existing correct record 
        public void PostExistingRecord()
        {
            defaultController.Configuration = new HttpConfiguration();

            PhoneBookModel bModel = new PhoneBookModel
            {
                PhoneID = 0,
                FirstName = "Eric",
                LastName = "Elliot",
                PhoneNumber = "222-555-6575"
            };

            IHttpActionResult actionResult = defaultController.Post(bModel);
            var FoundRes = actionResult as  OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Phone number already exists", FoundRes.Content);
        }

        [TestMethod]
        // update existing correct record 
        public void UpdateExistingRecord()
        {
            defaultController.Configuration = new HttpConfiguration();

            PhoneBookModel bModel = new PhoneBookModel
            {
                PhoneID = 27,
                FirstName = "Eric",
                LastName = "Elliot",
                PhoneNumber = "222-555-6575"
            };

            IHttpActionResult actionResult = defaultController.Put(bModel);
            var FoundRes = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Contact Updated", FoundRes.Content);
        }

        [TestMethod]
        // update existing phone number
        public void UpdateNonExistingRecord()
        {
            defaultController.Configuration = new HttpConfiguration();

            PhoneBookModel bModel = new PhoneBookModel
            {
                PhoneID = 27,
                FirstName = "Eric",
                LastName = "Elliot",
                PhoneNumber = "88677567891"
            };

            IHttpActionResult actionResult = defaultController.Put(bModel);
            var FoundRes = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Phone number already exists for another contact", FoundRes.Content);
        }


        [TestMethod]
        // delete existing phone number
        public void DeleteExistingRecord()
        {
            defaultController.Configuration = new HttpConfiguration();

            PhoneBookModel bModel = new PhoneBookModel
            {
                PhoneID = 27,
                FirstName = "Eric",
                LastName = "Elliot",
                PhoneNumber = "222-555-6575"
            };

            IHttpActionResult actionResult = defaultController.DeleteContact(bModel);
            var FoundRes = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Contact deleted", FoundRes.Content);
        }
    }
}
