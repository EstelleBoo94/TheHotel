using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Services.InvoiceServices;

namespace TheHotel.Displays.MenuFolder;

public class InvoiceMenu
{
    public void ShowInvoiceMenu(DbContextOptions<ApplicationDbContext> options)
    {
        List<string> menuOptions = new List<string>
        {
            "Visa alla fakturor", "Visa obetalda fakturor", "Visa betalda fakturor", "Ändra/betala faktura"
        };

        MenuTemplate.ShowMenu("Tillbaka", menuOptions, "FAKTURAMENY", selection =>
        {
            ReadInvoice readInvoice = new();

            switch (selection)
            {
                case 0:
                    readInvoice.Read(options);
                    break;

                case 1:
                    readInvoice.ReadNotPaid(options);
                    break;

                case 2:
                    readInvoice.ReadPaid(options);
                    break;

                case 3:
                    UpdateInvoice updateInvoice = new();
                    updateInvoice.Update(options);
                    break;
            }


        });

        MainMenu mainMenu = new MainMenu();
        mainMenu.ShowMainMenu(options);

    }
}
