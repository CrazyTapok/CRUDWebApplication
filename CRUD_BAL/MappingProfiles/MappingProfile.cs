using AutoMapper;
using CRUD_BAL.DTO;
using CRUD_DAL.Entities;

namespace CRUD_BAL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
        }
    }
}
