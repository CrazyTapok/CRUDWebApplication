using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_BAL.Interfaces;
using CRUD_BAL.DTO;
using CRUDAspNetCoreWebAPI.Models;

namespace CRUDAspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contact)
        {
            contactService = contact;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContacts()
        {
            var listContacts = await contactService.GetAllCompanies();

            return Ok(listContacts);
        }

        // GET: api/Contacts/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ContactDTO>> GetContact(Guid id)
        {
            var contact = await contactService.GetContactById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutContact(Guid id, ContactRequest contactRequest)
        {
            var contact = new ContactDTO()
            {
                Id = id,
                Name = contactRequest.Name,
                MobilePhone = contactRequest.MobilePhone,
                JobTitle = contactRequest.JobTitle,
                BirthDate = contactRequest.BirthDate
            };

            await contactService.UpdateContact(contact);
           
            return Ok(contact);
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<ContactDTO>> PostContact(ContactRequest contactRequest)
        {
            var contact = new ContactDTO()
            {
                Id = Guid.NewGuid(),
                Name = contactRequest.Name,
                MobilePhone = contactRequest.MobilePhone,
                JobTitle = contactRequest.JobTitle,
                BirthDate = contactRequest.BirthDate
            };

            await contactService.CreateContact(contact);

            return Ok(contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await contactService.GetContactById(id);

            if (contact != null)
            {
                await contactService.DeleteContact(id);

                return Ok(contact);
            }

            return NotFound();
        }
    }
}
