namespace TheHotel.Displays.MenuFolder;

public static class RoomTypeMenu
{
    public static int ShowRoomTypeMenu(string prompt, string headerText)
    {
        List<string> menuOptions = new List<string>
        {
        "Enkelrum", "Dubbelrum", "Svit"
        };

        int selection = MenuTemplate.ShowMenuWithReturn
            (prompt, headerText, menuOptions);

        switch (selection)
        {
            case 0:
                return 1;
            case 1:
                return 2;
            case 2:
                return 3;
            default:
                return 0;
        }

    }
}
