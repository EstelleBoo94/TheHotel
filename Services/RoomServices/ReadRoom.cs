using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.TablesFolder;

namespace TheHotel.Services.RoomServices;

public class ReadRoom
{
    public void Read(DbContextOptions<ApplicationDbContext> options)
    {
        Console.Clear();
        RoomTableClass.DisplayAllRoomsTable(options, "ALLA RUM");
        Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
        Console.ReadKey();
    }

}
