using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.User;

namespace Toyer.Data.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserPresentShortDto>().ReverseMap();
        CreateMap<User, UserPresentLongDto>().ReverseMap();
        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<PersonalInfo, UserPersonalInfoDto>().ReverseMap();
        CreateMap<UserCreateDto, PersonalInfo >().ReverseMap();
        CreateMap<UserCreateDto, AddressDto>().ReverseMap();
        CreateMap<UserAddressDto, AddressDto>().ReverseMap();
    }
}
