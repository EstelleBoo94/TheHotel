using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.GuestServices;

public class SearchGuest
{
    public void Search(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = GuestTableClass.DisplayAllGuestsTable(options, "SÖK GÄST");
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
                Console.WriteLine("Ange gästId för gästen du vill se mer information om:" +
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
                    findGuest = dbContext.Guests.Include(a => a.ActiveBookings).FirstOrDefault(g => g.GuestId == findId);
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
                GuestTableClass.DisplayOneGuestTable(findGuest, "VISA KUNDINFORMATION");
                Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                Console.ReadKey();
            }
        }
    }
}
