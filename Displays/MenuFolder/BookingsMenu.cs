using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Models;
using TheHotel.Services;

namespace TheHotel.Displays.MenuFolder
{
    public class BookingsMenu
    {
        public void ShowBookingMenu()
        {
            List<string> menuOptions = new List<string>
            {
            "Ny bokning", "Ändra bokning", "Sök bokning", "Avboka"
            };

            MenuTemplate.ShowMenu("Tillbaka", menuOptions, selection =>
            {
                switch (selection)
                {
                    case 0:
                        Console.WriteLine("Här finns ny bokning");
                        Console.ReadKey();
                        break;

                    case 1:
                        Console.WriteLine("Här finns ändra bokning");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Här finns sök bokning");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Här finns avboka");
                        Console.ReadKey();
                        break;
                }

            });

            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowMainMenu();

        }
    }
}
