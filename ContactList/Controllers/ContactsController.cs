using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContactList.Controllers
{
    [RoutePrefix("api/v1/contacts")]
    public class ContactsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public Task<IEnumerable<Contact>> GetContacts()
        {
            var contacts = new Contact[] { 
                new Contact { Id = 1, Name = "Nardi Rens", EmailAddress = "nardir@axerrio.com"},
                new Contact { Id = 2, Name = "Jos Janssen", EmailAddress = "josj@axerrio.com"},
                new Contact { Id = 3, Name = "Niels Dekkers", EmailAddress = "nielsd@axerrio.com"}
            };

            return Task.FromResult(contacts.AsEnumerable());
        }

        //[Route("{id:int}")]
        //[HttpGet]
        //public HttpResponseMessage GetContact(int Id)
        //{
        //    var response = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ObjectContent<Contact>(new Contact { Id = 1, Name = "Nardi Rens" }, new JsonMediaTypeFormatter(), "application/json")
        //    };

        //    return response;
        //}
    }
}
