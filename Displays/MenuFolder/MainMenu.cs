using Microsoft.EntityFrameworkCore;
using TheHotel.Data;

namespace TheHotel.Displays.MenuFolder;

public class MainMenu
{
    public void ShowMainMenu(DbContextOptions<ApplicationDbContext> options)
    {
        List<string> menuOptions = new List<string>
        {
        "Bokningar", "Gäster", "Rum", "Fakturor"
        };

        MenuTemplate.ShowMenu("Avsluta", menuOptions, "HUVUDMENY", selection =>
        {
            switch (selection)
            {
                case 0:
                    BookingMenu bookingsMenu = new();
                    bookingsMenu.ShowBookingMenu(options);
                    break;

                case 1:
                    GuestMenu customersMenu = new();
                    customersMenu.ShowGuestMenu(options);
                    break;
                case 2:
                    RoomMenu roomsMenu = new();
                    roomsMenu.ShowRoomMenu(options);
                    break;
                case 3:
                    InvoiceMenu invoiceMenu = new();
                    invoiceMenu.ShowInvoiceMenu(options);
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
