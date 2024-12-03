using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.MenuFolder
{
    public class MainMenu
    {
        public void ShowMainMenu()
        {
            List<string> menuOptions = new List<string>
            {
            "Ny kund", "Admin"
            };

            int selection = 0;
            bool inMenu = true;

            while (inMenu == true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Välj alternativ med piltangenterna:\n");
                Console.ResetColor();

                for (int i = 0; i < menuOptions.Count; i++)
                {
                    if (i == selection)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(menuOptions[i]);

                    Console.ResetColor();
                }

                if (selection == menuOptions.Count)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("Avsluta");
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
                        Console.WriteLine("Här finns kund");
                    }
                    else if (selection == 1)
                    {
                        Console.WriteLine("Här finns admin");
                    }
                }


            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tryck valfri tangent för att avsluta helt.");
            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(0);

        }
    }
}
