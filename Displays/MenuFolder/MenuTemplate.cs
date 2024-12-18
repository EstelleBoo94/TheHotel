using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Displays.MenuFolder
{
    public static class MenuTemplate
    {
        public static void ShowMenu(string back, List<string> options, Action<int> onOptionSelected)
        {
            int selection = 0;
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Välj alternativ med piltangenterna:\n");
                Console.ResetColor();

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selection)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                if (selection == options.Count)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{back}");
                Console.ResetColor();

                var keyInput = Console.ReadKey(true);

                if (keyInput.Key == ConsoleKey.UpArrow)
                {
                    selection = (selection - 1 + options.Count + 1) % (options.Count + 1);
                }
                else if (keyInput.Key == ConsoleKey.DownArrow)
                {
                    selection = (selection + 1) % (options.Count + 1);
                }
                else if (keyInput.Key == ConsoleKey.Enter)
                {
                    if (selection == options.Count)
                    {
                        inMenu = false;
                    }
                    else
                    {
                        onOptionSelected(selection);
                    }
                }
            }
        }
    }
}
