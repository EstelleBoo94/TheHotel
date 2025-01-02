using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.TablesFolder;

namespace TheHotel.Services.GuestServices;

public class ReadGuest
{
    public void Read(DbContextOptions<ApplicationDbContext> options)
    {
        Console.Clear();
        GuestTableClass.DisplayAllGuestsTable(options, "ALLA GÄSTER");
        Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
        Console.ReadKey();
    }
}
