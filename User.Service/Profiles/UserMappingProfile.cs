using AutoMapper;
using User.Domain.Enitites;
using User.Service.Models.User;
using User.Service.ViewModels.User;

namespace User.Service.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterModel, Domain.Entities.User>();
            CreateMap<LoginModel, Login>();
            CreateMap<Domain.Entities.User, UserViewModel>();
        }
    }
}
