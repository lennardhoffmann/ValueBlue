using AutoMapper;
using ExternalFilmService.Models;
using VB.API.Models;
using VB.Data.Models;

namespace VB.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RequestProperties, FilmRequest>()
                    .ForMember(dest => dest.Search_Token, opt => opt.MapFrom(src => src.FilmName))
                    .ForMember(dest => dest.Processing_Time_Ms, opt => opt.MapFrom(src => src.RequestResponseTime))
                    .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.TimeStamp))
                    .ForMember(dest => dest.IP_Address, opt => opt.MapFrom(src => src.UserIpAddress))
                    .ForMember(dest => dest.imdbID, opt => opt.MapFrom(src => src.ImdbID));
        }
    }
}
