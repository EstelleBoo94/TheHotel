using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Displays.TablesFolder;
using TheHotel.Services;

namespace TheHotel.Displays.MenuFolder
{
    public class CustomersMenu
    {
        public void ShowCustomerMenu(CustomerList customers)
        {
            CustomerServices customerServices = new();

            List<string> menuOptions = new List<string>
            {
            "Registrera ny kund", "Ändra kunduppgifter", "Visa kunder", "Sök kund", "Avaktivera/Aktivera kund"
            };

            MenuTemplate.ShowMenu("Tillbaka", menuOptions, selection =>
            {
                switch (selection)
                {
                    case 0:
                        customerServices.Create(customers);
                        break;

                    case 1:
                        customerServices.Update(customers);
                        break;
                    case 2:
                        customerServices.Read(customers);
                        break;
                    case 3:
                        customerServices.FindCustomer(customers);
                        break;
                    case 4:
                        customerServices.Delete(customers);
                        break;
                }


            });

            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowMainMenu();

        }
    }
}
