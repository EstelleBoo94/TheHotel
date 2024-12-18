using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

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
                AnsiConsole.Clear();
                Console.WriteLine("Välj alternativ med piltangenterna:\n");

                var content = new List<string>();
                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selection)
                    {
                        content.Add($"[black on pink1]{options[i]}[/]");
                    }
                    else
                    {
                        content.Add($" {options[i]}");
                    }
                }

                if (selection == options.Count)
                {
                    content.Add($"[black on red]{back}[/]");
                }
                else
                {
                    content.Add($" {back}");
                }
                
                var panel = new Panel(new Markup(string.Join("\n", content)))
                {
                    Padding = new Padding(1),
                    Border = BoxBorder.Double,
                    BorderStyle = Style.Parse("pink1"),
                };

                AnsiConsole.Write(panel);

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

        public static int ShowMenuWithReturn(string prompt, List<string> options)
        {
            int selection = 0;
            bool inMenu = true;

            while (inMenu)
            {
                AnsiConsole.Clear();
                Console.WriteLine($"{prompt}");
                Console.WriteLine("Välj alternativ med piltangenterna:\n");

                var content = new List<string>();
                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selection)
                    {
                        content.Add($"[black on pink1]{options[i]}[/]");
                    }
                    else
                    {
                        content.Add($" {options[i]}");
                    }
                }

                if (selection == options.Count)
                {
                    content.Add($"[black on red]Tillbaka[/]");
                }
                else
                {
                    content.Add($" Tillbaka");
                }

                var panel = new Panel(new Markup(string.Join("\n", content)))
                {
                    Padding = new Padding(1),
                    Border = BoxBorder.Double,
                    BorderStyle = Style.Parse("pink1"),
                };

                AnsiConsole.Write(panel);

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
                    return selection;
                }
            }
            return -1;
        }
    }
}
