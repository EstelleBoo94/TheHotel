using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Displays.MenuFolder
{
    public static class YesNoMenu
    {
        public static int ShowYesNoMenu(string prompt)
        {
            List<string> menuOptions = new List<string>
            {
            "Ja", "Nej"
            };

            int selection = MenuTemplate.ShowMenuWithReturn(prompt, menuOptions);

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
}
