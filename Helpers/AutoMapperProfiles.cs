using AutoMapper;
using WebApp_Core.Dto;
using WebApp_Core.Models;

namespace WebApp_Core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserForReturnDto>();
            CreateMap<UserForReturnDto, User>();
            CreateMap<PartForReturnDto, Part>();
            CreateMap<Part, PartForReturnDto>();
            CreateMap<GeneralPartsToReturnDto, Part>();
            CreateMap<Part, GeneralPartsToReturnDto>();
        }
    }
}