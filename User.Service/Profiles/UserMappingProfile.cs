using AutoMapper;
using User.Domain.DTO;

namespace User.Service.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterDto, Domain.Entities.User>();
        }
    }
}
