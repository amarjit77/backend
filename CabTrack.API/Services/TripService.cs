using AutoMapper;
using CabTrack.API.DTOs.Trips;
using CabTrack.API.Domain.Entities;
using CabTrack.API.Repositories;

namespace CabTrack.API.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;

    public TripService(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public async Task<TripDto> CreateTripAsync(int userId, TripCreateDto dto)
    {
        var trip = _mapper.Map<Trip>(dto);
        trip.UserId = userId;
        trip.CreatedDate = DateTime.UtcNow;

        await _tripRepository.AddAsync(trip);
        await _tripRepository.SaveChangesAsync();

        return _mapper.Map<TripDto>(trip);
    }

    public async Task<IEnumerable<TripDto>> GetTripsAsync(int userId)
    {
        var trips = await _tripRepository.GetAllByUserAsync(userId);
        return _mapper.Map<List<TripDto>>(trips);
    }

    public async Task<TripDto> GetTripByIdAsync(int userId, int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id, userId);
        if (trip == null) throw new KeyNotFoundException("Trip not found.");

        return _mapper.Map<TripDto>(trip);
    }

    public async Task UpdateTripAsync(int userId, int id, TripUpdateDto dto)
    {
        var trip = await _tripRepository.GetByIdAsync(id, userId);
        if (trip == null) throw new KeyNotFoundException("Trip not found.");

        _mapper.Map(dto, trip);
        await _tripRepository.UpdateAsync(trip);
        await _tripRepository.SaveChangesAsync();
    }

    public async Task DeleteTripAsync(int userId, int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id, userId);
        if (trip == null) throw new KeyNotFoundException("Trip not found.");

        await _tripRepository.DeleteAsync(trip);
        await _tripRepository.SaveChangesAsync();
    }

    public async Task<decimal> GetReportAsync(int userId, DateTime start, DateTime end)
    {
        var trips = await _tripRepository.GetTripsInDateRangeAsync(userId, start, end);
        return trips.Sum(t => t.Fare);
    }
}