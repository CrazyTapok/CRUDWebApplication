using AutoMapper;
using CRUD_BAL.DTO;
using CRUD_BAL.Infrastructure;
using CRUD_BAL.Interfaces;
using CRUD_DAL.EF;
using CRUD_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_BAL.Services
{
    public class ContactService : IContactService
    {

        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public ContactService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateContact(ContactDTO dto)
        {
            _context.Contacts.Add(_mapper.Map<Contact>(dto));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteContact(Guid? id)
        {
            if (!id.HasValue)
                throw new ValidationException("Contact ID not set", "");

            var contact = await _context.Contacts.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (contact == null)
                throw new ValidationException("Contact not found.", "");

            _context.Contacts.Remove(contact);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactDTO>> GetAllCompanies()
        {
            return await _mapper.ProjectTo<ContactDTO>(_context.Contacts).ToListAsync();
        }

        public async Task<ContactDTO> GetContactById(Guid? id)
        {
            if (!id.HasValue)
                throw new ValidationException("Contact ID not set", "");

            var contact = await _context.Contacts.FirstOrDefaultAsync(p => p.Id.Equals(id));

            return _mapper.Map<ContactDTO>(contact);
        }

        public async Task UpdateContact(ContactDTO dto)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(p => p.Id.Equals(dto.Id));

            if (contact != null)
            {
                contact.Name = dto.Name;
                contact.MobilePhone = dto.MobilePhone;
                contact.JobTitle = dto.JobTitle;
                contact.BirthDate = dto.BirthDate;

                _context.Contacts.Update(contact);

                await _context.SaveChangesAsync();
            }
            
        }
    }
}
