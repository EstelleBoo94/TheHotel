using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.GuestServices;

public class ActivateGuest
{
    public void Activate(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = GuestTableClass.DisplayAllDeactivatedGuestsTable(
                options, "AKTIVERA GÄST");
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
                Console.WriteLine("Ange gästId för gästen du vill aktivera:" +
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
                    findGuest = dbContext.Guests.FirstOrDefault(r => r.GuestId == findId);
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
                int deactivate = YesNoMenu.ShowYesNoMenu(
                    $"Är du säker på att du vill aktivera gäst med gästId {findGuest.GuestId}?", "AKTIVERA GÄST");
                if (deactivate == 1)
                {
                    findGuest.IsActive = true;
                    dbContext.SaveChanges();
                    Console.WriteLine("Gästen är aktiverad.");
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
