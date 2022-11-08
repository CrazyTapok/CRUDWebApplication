using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRUD_BAL.DTO;

namespace CRUD_BAL.Interfaces
{
    public interface IContactService
    {
        Task<ContactDTO> GetContactById(Guid? id);
        Task<IEnumerable<ContactDTO>> GetAllCompanies();
        Task UpdateContact(ContactDTO dto);
        Task CreateContact(ContactDTO dto);
        Task DeleteContact(Guid? id);
    }
}
