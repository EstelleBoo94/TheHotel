using Microsoft.EntityFrameworkCore;
using TheHotel.Data;

namespace TheHotel.Services.InvoiceServices;

public class InvoicePaymentCheck
{
    public void CheckPayment(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            var unpaidInvoices = dbContext.Invoices
                .Where(i => i.isPaid == false).ToList();

            if (unpaidInvoices.Any())
            {
                DateTime currentDateTime = DateTime.Now;
                DateOnly currentDate = DateOnly.FromDateTime(currentDateTime);
                var isItPastDueDate = unpaidInvoices
                    .Where(i => i.DueDate < currentDate).ToList();

                if (isItPastDueDate.Any())
                {
                    Console.WriteLine("Följande fakturor är förfallna och " +
                        "motsvarande bokningar har tagits bort:");
                    foreach (var invoice in isItPastDueDate)
                    {
                        Console.WriteLine($"Fakturanummer {invoice.InvoiceId}" +
                            $", bokningsId {invoice.BookingId}.");
                        var booking = dbContext.Bookings.Find(invoice.BookingId);
                        dbContext.Bookings.Remove(booking);
                        dbContext.SaveChanges();
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
