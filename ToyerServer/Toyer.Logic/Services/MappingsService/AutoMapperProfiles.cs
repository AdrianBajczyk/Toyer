﻿using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Device;
using Toyer.Logic.Dtos.DeviceType;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Dtos.User;

namespace Toyer.Logic.Services.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserPresentShortDto>().ReverseMap();
        CreateMap<User, UserPresentLongDto>().ReverseMap();
        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<PersonalInfo, PersonalInfoDto>().ReverseMap();
        CreateMap<UserCreateDto, PersonalInfo>().ReverseMap();
        CreateMap<UserCreateDto, Address>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();

        CreateMap<DeviceType, DeviceTypeCreateDto>().ReverseMap();
        CreateMap<DeviceType, DeviceTypePresentDto>().ReverseMap();

        CreateMap<Order, OrderCreateDto>().ReverseMap();
        CreateMap<Order, OrderPresentDto>().ReverseMap();
        CreateMap<Order, OrderAssignDto>().ReverseMap();

        CreateMap<Device, DeviceCreateDto>().ReverseMap();
        CreateMap<Device, DevicePresentDto>().ReverseMap();
        CreateMap<Device, DevicePresentShortDto>().ReverseMap();
        CreateMap<Device, DeviceNameUpdateDto>().ReverseMap();
    }
}
