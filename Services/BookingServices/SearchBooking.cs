using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.BookingServices;

public class SearchBooking
{
    public void Search(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = BookingTableClass.DisplayAllBookingsTable(options, "SÖK BOKNING");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int findId = 0;
            var findBooking = new Booking();

            while (!isValidInput)
            {
                Console.WriteLine("Ange bokningsId för bokningen du vill se mer information om:" +
                    "\nAnge 0 eller lämna tomt för att gå tillbaka");

                int.TryParse(Console.ReadLine(), out findId);
                if (findId == 0)
                {
                    return;
                }
                else if (findId < 0)
                {
                    Console.WriteLine("Ogiltigt bokningsId.");
                }
                else
                {
                    findBooking = dbContext.Bookings.Include(b => b.RoomsBooked).Include(i => i.Invoice).FirstOrDefault(b => b.BookingId == findId);
                     //dbContext.Bookings.FirstOrDefault(b => b.BookingId == findId);
                    if (findBooking == null)
                    {
                        Console.WriteLine("Bokningen hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            if (findBooking != null)
            {
                BookingTableClass.DisplayOneBookingTable(findBooking, "VISA BOKNINGSINFORMATION");
                Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                Console.ReadKey();
            }
        }
    }
}
