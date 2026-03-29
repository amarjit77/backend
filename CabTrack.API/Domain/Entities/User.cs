namespace CabTrack.API.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public List<Trip> Trips { get; set; } = new();
}