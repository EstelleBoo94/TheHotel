using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.TablesFolder;

namespace TheHotel.Services.InvoiceServices;

public class ReadInvoice
{
    public void Read(DbContextOptions<ApplicationDbContext> options)
    {
        Console.Clear();
        InvoiceTableClass.DisplayAllInvoicesTable(options, "ALLA FAKTUROR");
        Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
        Console.ReadKey();
    }

    public void ReadNotPaid(DbContextOptions<ApplicationDbContext> options)
    {
        Console.Clear();
        InvoiceTableClass.DisplayNotPaidInvoicesTable(options, "OBETALDA FAKTUROR");
        Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
        Console.ReadKey();
    }

    public void ReadPaid(DbContextOptions<ApplicationDbContext> options)
    {
        Console.Clear();
        InvoiceTableClass.DisplayPaidInvoicesTable(options, "BETALDA FAKTUROR");
        Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
        Console.ReadKey();
    }
}
