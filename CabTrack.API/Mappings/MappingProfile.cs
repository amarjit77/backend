using AutoMapper;
using CabTrack.API.Domain.Entities;
using CabTrack.API.DTOs.Trips;

namespace CabTrack.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TripCreateDto, Trip>();
        CreateMap<TripUpdateDto, Trip>();
        CreateMap<Trip, TripDto>();
    }
}