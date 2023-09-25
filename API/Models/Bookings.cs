namespace API.Models;

public class Bookings : BaseEntity
{
    public DateTime StartdDate { get; set; }
    public DateTime EnddDate { get; set; }
    public int Status { get; set; }
    public string Remarks { get; set; }
    public Guid Room { get; set; }
    public Guid Employee { get; set; }

}