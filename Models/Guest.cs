using System.ComponentModel.DataAnnotations;

namespace TheHotel.Models;

public class Guest
{
    public int GuestId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SSN { get; set; }

    [Phone]
    [MinLength(10)]
    public string PhoneNumber { get; set; }

    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public string Email { get; set; }
    public string StreetAddress { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<Booking> ActiveBookings { get; set; }
    public bool IsActive { get; set; }
}
