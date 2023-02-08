namespace Api.DTOs;

public class BookingRequest
{
    public int DoctorId { get; set; }
    public int PackageId { get; set; }
    public DateTime? Scheduler { get; set; }
}