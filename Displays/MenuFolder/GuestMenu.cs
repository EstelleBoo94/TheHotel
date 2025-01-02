using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Services.GuestServices;

namespace TheHotel.Displays.MenuFolder;

public class GuestMenu
{
    public void ShowGuestMenu(DbContextOptions<ApplicationDbContext> options)
    {
        List<string> menuOptions = new List<string>
        {
        "Registrera ny gäst", "Ändra gästinformation", "Visa gäster", 
            "Sök gäst", "Avaktivera gäst", "Aktivera gäst"
        };

        MenuTemplate.ShowMenu("Tillbaka", menuOptions, "GÄSTMENY", selection =>
        {
            switch (selection)
            {
                case 0:
                    CreateGuest createGuest = new();
                    createGuest.Create(options);
                    break;

                case 1:
                    UpdateGuest updateGuest = new();
                    updateGuest.Update(options);
                    break;
                case 2:
                    ReadGuest readGuest = new();
                    readGuest.Read(options);
                    break;
                case 3:
                    SearchGuest findGuest = new();
                    findGuest.Search(options);
                    break;
                case 4:
                    DeleteGuest deleteGuest = new();
                    deleteGuest.Delete(options);
                    break;
                case 5:
                    ActivateGuest activateGuest = new();
                    activateGuest.Activate(options);
                    break;
            }


        });

        MainMenu mainMenu = new MainMenu();
        mainMenu.ShowMainMenu(options);

    }
}
