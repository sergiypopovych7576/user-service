using AutoMapper;
using User.Domain.Enitites;
using User.Service.Models.User;

namespace User.Service.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterModel, Domain.Entities.User>();
            CreateMap<LoginModel, Login>();
        }
    }
}
