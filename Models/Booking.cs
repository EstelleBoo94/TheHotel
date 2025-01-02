using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheHotel.Models;

public class Booking
{
    [Key]
    public int BookingId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public List<Room> RoomsBooked { get; set; }

    [ForeignKey("Invoice")]
    public int? InvoiceId { get; set; }
    public Invoice Invoice { get; set; }

    [ForeignKey("Guest")]
    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    public int ExtraBeds { get; set; }
}
