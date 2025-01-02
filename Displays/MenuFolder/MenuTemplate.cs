using Spectre.Console;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Displays.MenuFolder;

public static class MenuTemplate
{
    public static void ShowMenu(string back, List<string> options,
        string headerText, Action<int> onOptionSelected)
    {

        int selection = 0;
        bool inMenu = true;

        while (inMenu)
        {
            AnsiConsole.Clear();

            Header.DisplayHeader($"{headerText}");
            Console.WriteLine("\nVälj alternativ med piltangenterna\nEnter för att välja");

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

    public static int ShowMenuWithReturn(string prompt, string headerText, List<string> options)
    {
        int selection = 0;
        bool inMenu = true;

        while (inMenu)
        {
            AnsiConsole.Clear();
            Header.DisplayHeader($"{headerText}");
            Console.WriteLine($"{prompt}");
            Console.WriteLine("\nVälj alternativ med piltangenterna\nEnter för att välja");

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

    public static void ShowMenuWithTable(string back, string whatTable, string headerText,
        List<string> options, List<string> contentList, Func<int, bool> onOptionSelected)
    {
        int selection = 0;
        bool inMenu = true;

        while (inMenu)
        {
            AnsiConsole.Clear();
            Header.DisplayHeader($"{headerText}");
            Console.WriteLine("\nVälj alternativ med piltangenterna\nEnter för att välja");

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

            if (whatTable == "guest")
            {
                GuestTableClass.DisplayGuestTable(contentList);
            }
            else if (whatTable == "room")
            {
                RoomTableClass.DisplayRoomTable(contentList);
            }
            else if (whatTable == "roomNoActive")
            {
                RoomTableClass.DisplayRoomTableWithoutActiveTab(contentList);
            }
            else if (whatTable == "invoice")
            {
                InvoiceTableClass.DisplayInvoiceTable(contentList);
            }

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
                    inMenu = onOptionSelected(selection);
                }
            }
        }
    }

    public static void ShowMenuWithTableForBooking(string back, string headerText, 
        List<string> options, List<string> contentList, List<Room> rooms, Func<int, bool> onOptionSelected)
    {
        int selection = 0;
        bool inMenu = true;

        while (inMenu)
        {
            AnsiConsole.Clear();
            Header.DisplayHeader($"{headerText}");
            Console.WriteLine("\nVälj alternativ med piltangenterna\nEnter för att välja");

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

            BookingTableClass.DisplayBookingTable(contentList, rooms);

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
                    inMenu = onOptionSelected(selection);
                }
            }
        }
    }
}
