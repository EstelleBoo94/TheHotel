using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using TheHotel.Data;
using TheHotel.Models;

namespace TheHotel.Displays.TablesFolder;

public class BookingTableClass
{
    public static void DisplayBookingTable
        (List<string> content, List<Room> bookedRooms)
    {

        var table = new Table();
        {
            table.Border = TableBorder.DoubleEdge;
            table.BorderStyle = Style.Parse("pink1");
        };
        var table2 = new Table();
        {
            table2.Border = TableBorder.DoubleEdge;
            table2.BorderStyle = Style.Parse("pink1");
        };

        table.AddColumn("[Pink1]GästId[/]");
        table.AddColumn("[Pink1]Antal gäster[/]");
        table.AddColumn("[Pink1]Antal rum[/]");
        table.AddColumn("[Pink1]Startdatum[/]");
        table.AddColumn("[Pink1]Slutdatum[/]");
        table.AddColumn("[Pink1]Antal extrasängar[/]");

        table2.AddColumn("[Pink1]RumsId[/]");
        table2.AddColumn("[Pink1]Rumsnummer[/]");
        table2.AddColumn("[Pink1]Rumstyp[/]");
        table2.AddColumn("[Pink1]Pris[/]");

        table.AddRow(content[0], content[1], content[2],
            content[3], content[4], content[5]);

        foreach (Room r in bookedRooms)
        {
            if (r.Type == RoomType.Single)
            {
                table2.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                    "Enkelrum", r.Price.ToString());
            }
            else if (r.Type == RoomType.Double)
            {
                table2.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                    "Dubbelrum", r.Price.ToString());
            }
            else if (r.Type == RoomType.Suite)
            {
                table2.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                    "Svit", r.Price.ToString());
            }
        }

        AnsiConsole.Write(table);
        AnsiConsole.Write(table2);
    }

    public static bool DisplayAllBookingsTable
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

            table.AddColumn("[Pink1]BokningsId[/]");
            table.AddColumn("[Pink1]Startdatum[/]");
            table.AddColumn("[Pink1]Slutdatum[/]");
            table.AddColumn("[Pink1]Antal gäster[/]");
            table.AddColumn("[Pink1]Fakturanummer[/]");
            table.AddColumn("[Pink1]GästId[/]");

            if (dbContext.Bookings.Count() == 0)
            {
                Console.WriteLine("Det finns inga bokningar.");
                return false;
            }
            else
            {
                foreach (Booking b in dbContext.Bookings)
                {

                    table.AddRow(b.BookingId.ToString(), b.StartDate.ToString(),
                        b.EndDate.ToString(), b.NumberOfGuests.ToString(), 
                        b.InvoiceId.ToString(), b.GuestId.ToString());
                }
                AnsiConsole.Write(table);
                return true;
            }

        }
    }

    public static void DisplayOneBookingTable(Booking b, string headerText)
    {
        Console.Clear();
        Header.DisplayHeader($"{headerText}");
        var table = new Table();
        {
            table.Border = TableBorder.DoubleEdge;
            table.BorderStyle = Style.Parse("pink1");
        };
        var table2 = new Table();
        {
            table2.Border = TableBorder.DoubleEdge;
            table2.BorderStyle = Style.Parse("pink1");
        };

        table.AddColumn("[Pink1]BokningsId[/]");
        table.AddColumn("[Pink1]Startdatum[/]");
        table.AddColumn("[Pink1]Slutdatum[/]");
        table.AddColumn("[Pink1]Antal gäster[/]");
        table.AddColumn("[Pink1]Fakturanummer[/]");
        table.AddColumn("[Pink1]GästId[/]");

        table2.AddColumn("[Pink1]RumsId[/]");
        table2.AddColumn("[Pink1]Rumsnummer[/]");
        table2.AddColumn("[Pink1]Rumstyp[/]");
        table2.AddColumn("[Pink1]Antal extrasängar[/]");
        table2.AddColumn("[Pink1]Total summa[/]");

        table.AddRow(b.BookingId.ToString(), b.StartDate.ToString(), b.EndDate.ToString(),
            b.NumberOfGuests.ToString(), b.InvoiceId.ToString(), b.GuestId.ToString());

        foreach (Room r in b.RoomsBooked)
        {
            if (r.Type == RoomType.Single)
            {
                table2.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(), "Enkelrum",
                    b.ExtraBeds.ToString(), b.Invoice.Amount.ToString());
            }
            else if (r.Type == RoomType.Double)
            {
                table2.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(), "Dubbelrum",
                    b.ExtraBeds.ToString(), b.Invoice.Amount.ToString());
            }
            else if (r.Type == RoomType.Suite)
            {
                table2.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(), "Svit",
                    b.ExtraBeds.ToString(), b.Invoice.Amount.ToString());
            }

        }


        AnsiConsole.Write(table);
        AnsiConsole.Write(table2);
    }
}
