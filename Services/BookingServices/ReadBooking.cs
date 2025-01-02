using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.TablesFolder;

namespace TheHotel.Services.BookingServices;

public class ReadBooking
{
    public void Read(DbContextOptions<ApplicationDbContext> options)
    {
        Console.Clear();
        BookingTableClass.DisplayAllBookingsTable(options, "VISA BOKNINGAR");
        Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
        Console.ReadKey();
    }
}
