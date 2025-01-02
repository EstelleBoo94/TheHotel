using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Services.InvoiceServices;

namespace TheHotel;

public class App
{
    public void Run()
    {
        var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
        var config = builder.Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);

        using (var dbContext = new ApplicationDbContext(options.Options))
        {
            var dataInitiaizer = new DataInitializer();
            dataInitiaizer.MigrateAndSeed(dbContext);
        }

        InvoicePaymentCheck invoicePaymentCheck = new();
        invoicePaymentCheck.CheckPayment(options.Options);

        MainMenu mainMenu = new();
        mainMenu.ShowMainMenu(options.Options);
    }
}
