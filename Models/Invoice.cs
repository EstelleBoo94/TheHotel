using System.ComponentModel.DataAnnotations.Schema;

namespace TheHotel.Models;

public class Invoice
{
    public int InvoiceId { get; set; }

    [ForeignKey("GuestId")]
    public int GuestId { get; set; }

    [ForeignKey("BookingId")]
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly DueDate { get; set; }
    public bool isPaid { get; set; }
}
