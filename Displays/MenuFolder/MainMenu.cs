using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Services;

namespace TheHotel.Displays.MenuFolder
{
    public class MainMenu
    {
        public void ShowMainMenu()
        {
            CustomerList customers = new();
            RoomList rooms = new();

            List<string> menuOptions = new List<string>
            {
            "Bokningar", "Kunder", "Rum"
            };

            MenuTemplate.ShowMenu("Avsluta", menuOptions, selection =>
            {
                switch (selection)
                {
                    case 0:
                        BookingsMenu bookingsMenu = new BookingsMenu();
                        bookingsMenu.ShowBookingMenu();
                        break;

                    case 1:
                        CustomersMenu customersMenu = new CustomersMenu();
                        customersMenu.ShowCustomerMenu(customers);
                        break;
                    case 2:
                        RoomsMenu roomsMenu = new RoomsMenu();
                        roomsMenu.ShowRoomMenu(rooms);
                        break;
                }

            });
                  
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tryck valfri tangent för att avsluta helt.");
            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(0);

        }
    }
}
