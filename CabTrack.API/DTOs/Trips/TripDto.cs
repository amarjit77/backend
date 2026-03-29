namespace CabTrack.API.DTOs.Trips;

public class TripDto
{
    public int Id { get; set; }
    public string PickupLocation { get; set; } = null!;
    public string DropLocation { get; set; } = null!;
    public DateTime PickupTime { get; set; }
    public DateTime DropTime { get; set; }
    public double Kilometers { get; set; }
    public decimal Fare { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UserId { get; set; }
}