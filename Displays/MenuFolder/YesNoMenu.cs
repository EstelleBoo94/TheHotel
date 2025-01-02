namespace TheHotel.Displays.MenuFolder;

public static class YesNoMenu
{
    public static int ShowYesNoMenu(string prompt, string headerText)
    {
        List<string> menuOptions = new List<string>
        {
        "Ja", "Nej"
        };

        int selection = MenuTemplate.ShowMenuWithReturn
            (prompt, headerText, menuOptions);

        switch (selection)
        {
            case 0:
                return 1;
            case 1:
                return 2;
            default:
                return 0;
        }

    }
}
