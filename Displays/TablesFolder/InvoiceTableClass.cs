using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using TheHotel.Data;
using TheHotel.Models;

namespace TheHotel.Displays.TablesFolder;

public class InvoiceTableClass
{
    public static void DisplayInvoiceTable(List<string> content)
    {

        var table = new Table();
        {
            table.Border = TableBorder.DoubleEdge;
            table.BorderStyle = Style.Parse("pink1");
        };

        table.AddColumn("[Pink1]Summa[/]");
        table.AddColumn("[Pink1]Förfallodatum[/]");
        table.AddColumn("[Pink1]Betalad[/]");

        if (content[2] == "True")
        {
            content[2] = "[Green]Betald[/]";
        }
        else if (content[2] == "False")
        {
            content[2] = "[Red]Obetald[/]";
        }
        table.AddRow(content[0], content[1], content[2]);

        AnsiConsole.Write(table);
    }

    public static bool DisplayAllInvoicesTable
        (DbContextOptions<ApplicationDbContext> options, string headerText)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            Console.Clear();
            Header.DisplayHeader($"{headerText}");
            var table = new Table();
            {
                table.Border = TableBorder.DoubleEdge;
                table.BorderStyle = Style.Parse("pink1");
            };

            table.AddColumn("[Pink1]Fakturanummer[/]");
            table.AddColumn("[Pink1]Bokad gästId[/]");
            table.AddColumn("[Pink1]BokningsId[/]");
            table.AddColumn("[Pink1]Summa[/]");
            table.AddColumn("[Pink1]Förfallodatum[/]");
            table.AddColumn("[Pink1]Betalad[/]");

            if (dbContext.Invoices.Count() == 0)
            {
                Console.WriteLine("Det finns inga fakturor.");
                return false;
            }
            else
            {
                foreach (Invoice i in dbContext.Invoices)
                {
                    if (i.isPaid == false)
                    {
                        table.AddRow(i.InvoiceId.ToString(), i.GuestId.ToString(), i.BookingId.ToString(),
                            i.Amount.ToString(), i.DueDate.ToString(), "[Red]Obetald[/]");
                    }
                    else
                    {
                        table.AddRow(i.InvoiceId.ToString(), i.GuestId.ToString(), i.BookingId.ToString(),
                            i.Amount.ToString(), i.DueDate.ToString(), "[Green]Betald[/]");

                    }
                }
                AnsiConsole.Write(table);
                return true;
            }

        }
    }

    public static void DisplayNotPaidInvoicesTable
        (DbContextOptions<ApplicationDbContext> options, string headerText)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            Console.Clear();
            Header.DisplayHeader($"{headerText}");
            var table = new Table();
            {
                table.Border = TableBorder.DoubleEdge;
                table.BorderStyle = Style.Parse("pink1");
            };

            table.AddColumn("[Pink1]Fakturanummer[/]");
            table.AddColumn("[Pink1]Bokad gästId[/]");
            table.AddColumn("[Pink1]BokningsId[/]");
            table.AddColumn("[Pink1]Summa[/]");
            table.AddColumn("[Pink1]Förfallodatum[/]");

            if (dbContext.Invoices.Count() == 0)
            {
                Console.WriteLine("Det finns inga obetalda fakturor.");
            }
            else
            {
                foreach (Invoice i in dbContext.Invoices.Where(i => i.isPaid == false))
                {
                    table.AddRow(i.InvoiceId.ToString(), i.GuestId.ToString(), i.BookingId.ToString(),
                        i.Amount.ToString(), i.DueDate.ToString());
                }
                AnsiConsole.Write(table);
            }
        }
    }

    public static void DisplayPaidInvoicesTable
        (DbContextOptions<ApplicationDbContext> options, string headerText)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            Console.Clear();
            Header.DisplayHeader($"{headerText}");
            var table = new Table();
            {
                table.Border = TableBorder.DoubleEdge;
                table.BorderStyle = Style.Parse("pink1");
            };

            table.AddColumn("[Pink1]Fakturanummer[/]");
            table.AddColumn("[Pink1]Bokad gästId[/]");
            table.AddColumn("[Pink1]BokningsId[/]");
            table.AddColumn("[Pink1]Summa[/]");
            table.AddColumn("[Pink1]Förfallodatum[/]");

            if (dbContext.Invoices.Count() == 0)
            {
                Console.WriteLine("Det finns inga betalda fakturor.");
            }
            else
            {
                foreach (Invoice i in dbContext.Invoices.Where(i => i.isPaid == true))
                {
                    table.AddRow(i.InvoiceId.ToString(), i.GuestId.ToString(), i.BookingId.ToString(),
                        i.Amount.ToString(), i.DueDate.ToString());
                }
                AnsiConsole.Write(table);
            }
        }
    }
}
