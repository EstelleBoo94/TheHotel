using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Services.BookingServices;

namespace TheHotel.Displays.MenuFolder;

public class BookingMenu
{
    public void ShowBookingMenu(DbContextOptions<ApplicationDbContext> options)
    {
        List<string> menuOptions = new List<string>
        {
        "Ny bokning", "Ändra bokning", "Visa alla bokningar", "Sök bokning", "Avboka"
        };

        MenuTemplate.ShowMenu("Tillbaka", menuOptions, "BOKNINGSMENY", selection =>
        {
            switch (selection)
            {
                case 0:
                    CreateBooking createBooking = new();
                    createBooking.Create(options);
                    break;

                case 1:
                    UpdateBooking updateBooking = new();
                    updateBooking.Update(options);
                    break;
                case 2:
                    ReadBooking readBooking = new();
                    readBooking.Read(options);
                    break;
                case 3:
                    SearchBooking searchBooking = new();
                    searchBooking.Search(options);
                    break;
                case 4:
                    DeleteBooking deleteBooking = new();
                    deleteBooking.Delete(options);
                    break;
            }

        });

        MainMenu mainMenu = new MainMenu();
        mainMenu.ShowMainMenu(options);

    }
}
