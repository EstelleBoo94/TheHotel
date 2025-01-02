using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.GuestServices;

public class DeleteGuest
{
    public void Delete(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = GuestTableClass.DisplayAllActiveGuestsTable(
                options, "AVAKTIVERA GÄST");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int findId = 0;
            var findGuest = new Guest();

            while (!isValidInput)
            {
                Console.WriteLine("Ange gästId för gästen du vill avaktivera:" +
                    "\nAnge 0 eller lämna tomt för att gå tillbaka");

                int.TryParse(Console.ReadLine(), out findId);
                if (findId == 0)
                {
                    return;
                }
                else if (findId < 0)
                {
                    Console.WriteLine("Ogiltigt gästId.");
                }
                else
                {
                    findGuest = dbContext.Guests.FirstOrDefault(g => g.GuestId == findId);
                    if (findGuest == null)
                    {
                        Console.WriteLine("Gästen hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            if (findGuest != null)
            {
                var doesGuestHaveBooking = dbContext.Bookings
                    .Any(b => b.GuestId == findGuest.GuestId);
                if (doesGuestHaveBooking == true)
                {
                    Console.WriteLine("Gästen har aktiva bokningar och kan inte avaktiveras.\n" +
                        "Avboka gästens bokningar först.");
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                    Console.ReadKey();
                    return;
                }

                int deactivate = YesNoMenu.ShowYesNoMenu($"Är du säker på att du vill" +
                    $" avaktivera gäst med gästId {findGuest.GuestId}?", "AVAKTIVERA GÄST");
                if (deactivate == 1)
                {
                    findGuest.IsActive = false;
                    dbContext.SaveChanges();
                    Console.WriteLine("Gästen är avaktiverad.");
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                    Console.ReadKey();
                }
                else if (deactivate == 2)
                {
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                    Console.ReadKey();
                }

            }
        }
    }
}
