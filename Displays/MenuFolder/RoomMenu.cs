using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Services.RoomServices;

namespace TheHotel.Displays.MenuFolder;

public class RoomMenu
{
    public void ShowRoomMenu(DbContextOptions<ApplicationDbContext> options)
    {
        List<string> menuOptions = new List<string>
        {
        "Visa rum", "Ändra rum", "Avaktivera rum", "Aktivera rum", "Nytt rum"
        };

        MenuTemplate.ShowMenu("Tillbaka", menuOptions, "RUMSMENY", selection =>
        {
            switch (selection)
            {
                case 0:
                    ReadRoom readRoom = new();
                    readRoom.Read(options);
                    break;

                case 1:
                    UpdateRoom updateRoom = new();
                    updateRoom.Update(options);
                    break;
                case 2:
                    DeleteRoom deleteRoom = new();
                    deleteRoom.Delete(options);
                    break;
                case 3:
                    ActivateRoom activateRoom = new();
                    activateRoom.Activate(options);
                    break;
                case 4:
                    CreateRoom createRoom = new();
                    createRoom.Create(options);
                    break;
            }


        });

        MainMenu mainMenu = new MainMenu();
        mainMenu.ShowMainMenu(options);

    }
}
