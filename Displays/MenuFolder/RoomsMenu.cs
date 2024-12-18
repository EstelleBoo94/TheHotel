using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Models;
using TheHotel.Services;

namespace TheHotel.Displays.MenuFolder
{
    public class RoomsMenu
    {
        public void ShowRoomMenu(RoomList rooms)
        {
            RoomServices roomServices = new();

            List<string> menuOptions = new List<string>
            {
            "Visa rum", "Ändra rum", "Aktivera/avaktivera rum", "Skapa nytt rum"
            };

            MenuTemplate.ShowMenu("Tillbaka", menuOptions, selection =>
            {
                switch (selection)
                {
                    case 0:
                        Console.WriteLine("Här finns visa rum");
                        Console.ReadKey();
                        break;

                    case 1:
                        Console.WriteLine("Här finns ändra rum");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Här finns aktivera/avaktivera rum");
                        Console.ReadKey();
                        break;
                    case 3:
                        roomServices.Create(rooms);
                        break;
                }


            });

            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowMainMenu();

        }
    }
}
