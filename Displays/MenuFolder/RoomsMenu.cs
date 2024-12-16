using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            int selection = 0;
            bool inMenu = true;

            while (inMenu == true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Välj alternativ med piltangenterna:\n");
                Console.ResetColor();

                for (int i = 0; i < menuOptions.Count; i++)
                {
                    if (i == selection)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(menuOptions[i]);

                    Console.ResetColor();
                }

                if (selection == menuOptions.Count)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("Tillbaka");
                Console.ResetColor();


                var keyInput = Console.ReadKey(true);

                if (keyInput.Key == ConsoleKey.UpArrow)
                {
                    selection--;
                    if (selection < 0)
                    {
                        selection = menuOptions.Count;
                    }
                }

                else if (keyInput.Key == ConsoleKey.DownArrow)
                {
                    selection++;
                    if (selection > menuOptions.Count)
                    {
                        selection = 0;
                    }
                }

                else if (keyInput.Key == ConsoleKey.Enter)
                {
                    if (selection == menuOptions.Count)
                    {
                        inMenu = false;
                    }
                    else if (selection == 0)
                    {
                        Console.WriteLine("Här finns visa rum");
                        Console.ReadKey();
                    }
                    else if (selection == 1)
                    {
                        Console.WriteLine("Här finns ändra rum");
                        Console.ReadKey();
                    }
                    else if (selection == 2)
                    {
                        Console.WriteLine("Här finns aktivera/avaktivera rum");
                        Console.ReadKey();
                    }
                    else if (selection == 3)
                    {
                        roomServices.Create(rooms);
                    }
                }


            }

            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowMainMenu();

        }
    }
}
