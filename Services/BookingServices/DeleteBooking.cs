using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.BookingServices;

public class DeleteBooking
{
    public void Delete(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = BookingTableClass.DisplayAllBookingsTable(options, "AVBOKA");
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
                Console.WriteLine("Ange bokningsId för bokningen du vill ta bort:" +
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
                    findBooking = dbContext.Bookings.First(b => b.BookingId == findId);
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
                int remove = YesNoMenu.ShowYesNoMenu(
                    $"Är du säker på att du vill ta bort bokning med bokningsId {findBooking.BookingId}?", "AVBOKA");
                if (remove == 1)
                {
                    dbContext.Invoices.Remove(dbContext.Invoices
                        .First(i => i.BookingId == findBooking.BookingId));
                    dbContext.SaveChanges();
                    dbContext.Bookings.Remove(findBooking);
                    dbContext.SaveChanges();
                    Console.WriteLine("Bokningen och tillhörande faktura är borttagen.");
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                    Console.ReadKey();
                }
                else if (remove == 2)
                {
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                    Console.ReadKey();
                }

            }
        }
    }
}
