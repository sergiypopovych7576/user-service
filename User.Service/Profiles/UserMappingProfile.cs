using AutoMapper;
using User.Service.Models.Register;

namespace User.Service.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterModel, Domain.Entities.User>();
        }
    }
}
