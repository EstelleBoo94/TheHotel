using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Models;

namespace TheHotel.Services.InvoiceServices;

public class CreateInvoice
{
    public int Create(DbContextOptions<ApplicationDbContext> options,
        int guestId, int bookingId, List<Room> bookedRooms, DateOnly startDate)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            DateTime currentDateTime = DateTime.Now;
            DateOnly currentDate = DateOnly.FromDateTime(currentDateTime);
            var dueDate = currentDate.AddDays(10);

            if (currentDate.AddDays(10) > startDate)
            {
                dueDate = startDate;
            }
            var invoice = new Invoice
            {
                GuestId = guestId,
                BookingId = bookingId,
                Amount = bookedRooms.Sum(r => r.Price),
                DueDate = dueDate,
            };
            dbContext.Invoices.Add(invoice);
            dbContext.SaveChanges();

            return invoice.InvoiceId;
        }
    }
}

