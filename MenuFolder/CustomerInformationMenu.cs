using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.TablesFolder;

namespace TheHotel.MenuFolder
{
    public class CustomerInformationMenu
    {
        public void ShowCustomerInfoMenu()
        {
            List<string> menuOptions = new List<string>
            {
            "KundId", "Förnamn", "Efternamn", "Personnummer",
                "Telefonummer", "Email", "Gatuadress", "Postnummer", "Stad", "Land"
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

                Console.WriteLine("\n\n");
                CustomerTableClass.CustomerTable();

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
                        Console.WriteLine("Här finns ny kund");
                        Console.ReadKey();
                    }
                    else if (selection == 1)
                    {
                        Console.WriteLine("Här finns ändra kund");
                        Console.ReadKey();
                    }
                    else if (selection == 2)
                    {
                        Console.WriteLine("Här finns sök kund");
                        Console.ReadKey();
                    }
                    else if (selection == 3)
                    {
                        Console.WriteLine("Här finns ta bort kund");
                        Console.ReadKey();
                    }
                    else if (selection == 4)
                    {
                        Console.WriteLine("Här finns ta bort kund");
                        Console.ReadKey();
                    }
                    else if (selection == 5)
                    {
                        Console.WriteLine("Här finns ta bort kund");
                        Console.ReadKey();
                    }
                    else if (selection == 6)
                    {
                        Console.WriteLine("Här finns ta bort kund");
                        Console.ReadKey();
                    }
                    else if (selection == 7)
                    {
                        Console.WriteLine("Här finns ta bort kund");
                        Console.ReadKey();
                    }
                    else if (selection == 8)
                    {
                        Console.WriteLine("Här finns ta bort kund");
                        Console.ReadKey();
                    }
                    else if (selection == 9)
                    {
                        Console.WriteLine("Här finns ta bort kund");
                        Console.ReadKey();
                    }
                }


            }

            CustomersMenu customersMenu = new CustomersMenu();
            customersMenu.ShowCustomerMenu();

        }
    }
}
