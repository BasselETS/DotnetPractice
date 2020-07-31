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
            CreateMap<PartForReturnDto, Part>().ForMember(dest => dest.Amount,
             opt => opt.MapFrom(src => src.Amount));
            //.ForMember(dest => dest.PhotoUrl,
            // opt =>opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            CreateMap<Part, PartForReturnDto>().ForMember(dest => dest.Amount,
             opt => opt.MapFrom(src => src.Amount));
            CreateMap<GeneralPartsToReturnDto, Part>();
            CreateMap<Part, GeneralPartsToReturnDto>();
        }
    }
}