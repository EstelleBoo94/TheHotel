using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.RoomServices;

public class DeleteRoom
{
    public void Delete(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = RoomTableClass.DisplayAllActiveRoomsTable(options, "AVAKTIVERA RUM");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int findId = 0;
            var findRoom = new Room();

            while (!isValidInput)
            {
                Console.WriteLine("Ange rumsId för rummet du vill avaktivera:" +
                    "\nAnge 0 eller lämna tomt för att gå tillbaka");

                int.TryParse(Console.ReadLine(), out findId);
                if (findId == 0)
                {
                    return;
                }
                else if (findId < 0)
                {
                    Console.WriteLine("Ogiltigt rumsId.");
                }
                else
                {
                    findRoom = dbContext.Rooms.FirstOrDefault(r => r.RoomId == findId);
                    if (findRoom == null)
                    {
                        Console.WriteLine("Rummet hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            if (findRoom != null)
            {
                int deactivate = YesNoMenu.ShowYesNoMenu($"Är du säker på att du vill avaktivera" +
                    $" rum med rumsId {findRoom.RoomId}?", "AVAKTIVERA RUM");
                if (deactivate == 1)
                {
                    findRoom.IsActive = false;
                    dbContext.SaveChanges();
                    Console.WriteLine("Rummet är avaktiverat.");
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
