using AutoMapper;
using ProviderOne.Models;
using ProviderTwo.Models;
using SearchApi.Models;
using Shared.Models.Providers.ProviderOne;
using Route = SearchApi.Models.Route;

namespace SearchApi.Profiles;

public class ProviderProfile : Profile
{
    public ProviderProfile()
    {
        // Provider One
        CreateMap<ProviderOneRoute, Route>()
            .ForMember(
                dest => dest.Origin,
                opt => opt.MapFrom(
                    src => src.From))
            .ForMember(
                dest => dest.OriginDateTime,
                opt => opt.MapFrom(
                    src => src.DateFrom))
            .ForMember(
                dest => dest.Destination,
                opt => opt.MapFrom(
                    src => src.To))
            .ForMember(
                dest => dest.DestinationDateTime,
                opt => opt.MapFrom(
                    src => src.DateTo))
            .ForMember(
                dest => dest.Price,
                opt => opt.MapFrom(
                    src => src.Price))
            .ForMember(
                dest => dest.TimeLimit,
                opt => opt.MapFrom(
                    src => src.TimeLimit))
            .ReverseMap();
        
        
        CreateMap<SearchRequest, ProviderOneSearchRequest>()
            .ForMember(
                dest => dest.From,
                opt => opt.MapFrom(
                    src => src.Origin))
            .ForMember(
                dest => dest.DateFrom,
                opt => opt.MapFrom(
                    src => src.OriginDateTime))
            .ForMember(
                dest => dest.To,
                opt => opt.MapFrom(
                    src => src.Destination))
            .ForMember(
                dest => dest.DateTo,
                opt => opt.MapFrom(
                    src => src.Filters != null ? src.Filters.DestinationDateTime : null))
            .ForMember(
                dest => dest.MaxPrice,
                opt => opt.MapFrom(
                    src => src.Filters != null ? src.Filters.MaxPrice : null))
            .ReverseMap();
        

        // Provider Two
        CreateMap<SearchRequest, ProviderTwoSearchRequest>()
            .ForMember(
                dest => dest.Departure,
                opt => opt.MapFrom(
                    src => src.Origin))
            .ForMember(
                dest => dest.DepartureDate,
                opt => opt.MapFrom(
                    src => src.OriginDateTime))
            .ForMember(
                dest => dest.Arrival,
                opt => opt.MapFrom(
                    src => src.Destination))
            .ForMember(
                dest => dest.MinTimeLimit,
                opt => opt.MapFrom(
                    src => src.Filters != null ? src.Filters.MinTimeLimit : null))
            .ReverseMap();
        
        CreateMap<ProviderTwoRoute, Route>()
            .ForMember(
                dest => dest.Origin,
                opt => opt.MapFrom(
                    src => src.Departure.Point))
            .ForMember(
                dest => dest.OriginDateTime,
                opt => opt.MapFrom(
                    src => src.Departure.Date))
            .ForMember(
                dest => dest.Destination,
                opt => opt.MapFrom(
                    src => src.Arrival.Point))
            .ForMember(
                dest => dest.DestinationDateTime,
                opt => opt.MapFrom(
                    src => src.Arrival.Date))
            .ForMember(
                dest => dest.Price,
                opt => opt.MapFrom(
                    src => src.Price))
            .ForMember(
                dest => dest.TimeLimit,
                opt => opt.MapFrom(
                    src => src.TimeLimit))
            .ReverseMap();;
        
        
    }
}