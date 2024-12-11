using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }

        public Booking(int bookingId, DateOnly startDate,
            DateOnly endDate, int numberOfGuests, int roomNumber, decimal price)
        {
            BookingId = bookingId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfGuests = numberOfGuests;
            RoomNumber = roomNumber;
            Price = price;
        }
    }
}
