using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.InvoiceServices;

public class UpdateInvoice
{
    public void Update(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = InvoiceTableClass.DisplayAllInvoicesTable
                (options, "ÄNDRA FAKTURA");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int findId = 0;
            var findInvoice = new Invoice();

            while (!isValidInput)
            {
                Console.WriteLine("Ange fakturanummer för fakturan du vill uppdatera:" +
                    "\nAnge 0 eller lämna tomt för att gå tillbaka");

                int.TryParse(Console.ReadLine(), out findId);
                if (findId == 0)
                {
                    return;
                }
                else if (findId < 0)
                {
                    Console.WriteLine("Ogiltigt fakturanummer.");
                }
                else
                {
                    findInvoice = dbContext.Invoices.FirstOrDefault(i => i.InvoiceId == findId);
                    if (findInvoice == null)
                    {
                        Console.WriteLine("Fakturan hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }

            }

            if (findInvoice != null)
            {
                decimal amount = findInvoice.Amount;
                DateOnly dueDate = findInvoice.DueDate;
                bool isPaid = findInvoice.isPaid;

                var contentList = new List<string>
                {
                    findInvoice.Amount.ToString(), findInvoice.DueDate.ToString(),
                    findInvoice.isPaid.ToString(),
                };

                List<string> menuOptions = new List<string>
                 {
                    "Summa", "Förfallodatum", "Betala", "Spara faktura"
                };

                MenuTemplate.ShowMenuWithTable("Tillbaka", "invoice", "ÄNDRA FAKTURA",
                    menuOptions, contentList, selection =>
                {
                    switch (selection)
                    {
                        case 0:
                            Console.WriteLine("Ange summa:");
                            decimal.TryParse(Console.ReadLine(), out amount);
                            if (amount <= 0)
                            {
                                Console.WriteLine("Ogiltig summa.\nTryck för att fortsätta.");
                                Console.ReadKey();
                            }
                            else
                            {
                                contentList[0] = amount.ToString();
                            }
                            break;

                        case 1:
                            Console.WriteLine("Ange förfallodatum (YYYY-MM-DD):");
                            DateOnly.TryParse(Console.ReadLine(), out dueDate);
                            if (dueDate < DateOnly.FromDateTime(DateTime.Now))
                            {
                                Console.WriteLine("Ange korrekt datum.\nTryck för att fortsätta.");
                                Console.ReadKey();
                                break;
                            }

                            var findBooking = dbContext.Bookings
                            .FirstOrDefault(b => b.BookingId == findInvoice.BookingId);
                            if (dueDate >= findBooking.StartDate)
                            {
                                Console.WriteLine($"Förfallodatumet måste vara minst en dag innan bokningens startdatum." +
                                    $"\nBokningens startdatum är {findBooking.StartDate}");
                            }
                            else
                            {
                                contentList[1] = dueDate.ToString();
                            }
                            break;

                        case 2:
                            var payment = YesNoMenu.ShowYesNoMenu("Är fakturan betald?", "ÄNDRA FAKTURA");
                            if (payment == 1)
                            {
                                isPaid = true;
                                contentList[2] = isPaid.ToString();
                            }
                            else
                            {
                                isPaid = false;
                                contentList[2] = isPaid.ToString();
                            }
                            break;

                        case 3:
                            findInvoice.Amount = amount;
                            findInvoice.DueDate = dueDate;
                            findInvoice.isPaid = isPaid;

                            dbContext.SaveChanges();
                            Console.WriteLine("Fakturan är uppdaterad." +
                                "\nTryck valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            return false;
                    }
                    return true;
                });

            }
        }
    }
}
