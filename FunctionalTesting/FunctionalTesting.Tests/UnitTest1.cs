using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunctionalTesting.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            GetContact()
                //.OnSuccessAndWhen(
                //    c => c.Id == -1,
                //    () => CreateContact(),
                //    () => Log("No contact was created"));

                .OnSuccess(c => c.Id == -1 ? CreateContact().Cast() : Log("No contact was created"))
                .OnSuccess(CreateMsisdn)
                .When(r => r.IsFailure, r =>
                {
                    throw new Exception($"An exception has occured in API {r.ApiCall}, error code {r.ErrorCode}");
                });


            //.OnSuccess(c => c.Id == -1, () => CreateContact());

        }

        private static ApiResult<Contact> GetContact()
        {
            var contact = new Contact {Id = -1};

            return ApiResult.Ok("GetContact", contact);

            //return ApiResult.Fail<Contact>("GetContact", "6");
        }

        private static ApiResult<Contact> CreateContact()
        {
            var contact = new Contact { Id = 1, Name = "NewContact" };

            return ApiResult.Ok("CreateContact", contact);
        }

        private static ApiResult Log(string message)
        {
            return ApiResult.Ok("Log");
        }

        private static ApiResult CreateMsisdn()
        {
            //return ApiResult.Ok("CreateMsisdn");

            return ApiResult.Fail("CreateMsisdn", "2");
        } 

        private class Contact
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}