namespace TheHotel.Models;

public enum RoomType
{
    Single,
    Double,
    Suite
}

public class Room
{
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }
    public RoomType Type { get; set; }
    public int Size { get; set; }
    public decimal Price { get; set; }
    public List<Booking> ActiveBookings { get; set; }
    public bool IsActive { get; set; }
}
