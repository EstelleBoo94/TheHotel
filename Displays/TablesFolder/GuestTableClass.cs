using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using TheHotel.Data;
using TheHotel.Models;

namespace TheHotel.Displays.TablesFolder;

public class GuestTableClass
{
    public static void DisplayGuestTable(List<string> content)
    {

        var table = new Table();
        {
            table.Border = TableBorder.DoubleEdge;
            table.BorderStyle = Style.Parse("pink1");
        };

        table.AddColumn("[Pink1]Förnamn[/]");
        table.AddColumn("[Pink1]Efternamn[/]");
        table.AddColumn("[Pink1]Personnummer[/]");
        table.AddColumn("[Pink1]Telefon[/]");
        table.AddColumn("[Pink1]Email[/]");
        table.AddColumn("[Pink1]Gatuadress[/]");
        table.AddColumn("[Pink1]Postnummer[/]");
        table.AddColumn("[Pink1]Stad[/]");
        table.AddColumn("[Pink1]Land[/]");

        table.AddRow(content[0], content[1], content[2], content[3],
            content[4], content[5], content[6], content[7], content[8]);

        AnsiConsole.Write(table);
    }

    public static bool DisplayAllGuestsTable
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

            table.AddColumn("[Pink1]KundId[/]");
            table.AddColumn("[Pink1]Förnamn[/]");
            table.AddColumn("[Pink1]Efternamn[/]");
            table.AddColumn("[Pink1]Telefon[/]");
            table.AddColumn("[Pink1]Email[/]");
            table.AddColumn("[Pink1]Antal aktiva bokningar[/]");

            if (dbContext.Guests.Count() == 0)
            {
                Console.WriteLine("Det finns inga gäster.");
                return false;
            }
            else
            {
                foreach (Guest g in dbContext.Guests.Include(a => a.ActiveBookings))
                {
                    if (g.ActiveBookings == null)
                    {
                        table.AddRow(g.GuestId.ToString(), g.FirstName, g.LastName,
                             g.PhoneNumber.ToString(), g.Email, "0");
                    }
                    else
                    {
                        table.AddRow(g.GuestId.ToString(), g.FirstName, g.LastName,
                            g.PhoneNumber.ToString(), g.Email, g.ActiveBookings.Count.ToString());
                    }

                }

                AnsiConsole.Write(table);
                return true;
            }
        }
    }

    public static void DisplayOneGuestTable(Guest g, string headerText)
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

        table.AddColumn("[Pink1]KundId[/]");
        table.AddColumn("[Pink1]Förnamn[/]");
        table.AddColumn("[Pink1]Efternamn[/]");
        table.AddColumn("[Pink1]Personnummer[/]");
        table.AddColumn("[Pink1]Telefon[/]");
        table.AddColumn("[Pink1]Email[/]");

        table2.AddColumn("[Pink1]Gatuadress[/]");
        table2.AddColumn("[Pink1]Postnummer[/]");
        table2.AddColumn("[Pink1]Stad[/]");
        table2.AddColumn("[Pink1]Land[/]");
        table2.AddColumn("[Pink1]Antal aktiva bokningar[/]");

        table.AddRow(g.GuestId.ToString(), g.FirstName, g.LastName, g.SSN.ToString(),
                        g.PhoneNumber.ToString(), g.Email);

        if (g.ActiveBookings == null)
        {
            table2.AddRow(g.StreetAddress, g.PostalCode.ToString(), g.City, g.Country, "0");
        }
        else
        {
            table2.AddRow(g.StreetAddress, g.PostalCode.ToString(), g.City, g.Country,
                g.ActiveBookings.Count.ToString());
        }

        AnsiConsole.Write(table);
        AnsiConsole.Write(table2);
    }

    public static bool DisplayAllDeactivatedGuestsTable
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

            table.AddColumn("[Pink1]KundId[/]");
            table.AddColumn("[Pink1]Förnamn[/]");
            table.AddColumn("[Pink1]Efternamn[/]");
            table.AddColumn("[Pink1]Telefon[/]");
            table.AddColumn("[Pink1]Email[/]");

            if (dbContext.Guests.Where(g => g.IsActive == false).Count() == 0)
            {
                Console.WriteLine("Det finns inga avaktiverade gäster.");
                return false;
            }
            else
            {
                foreach (Guest g in dbContext.Guests.Where(g => g.IsActive == false))
                {
                    if (g.ActiveBookings == null)
                    {
                        table.AddRow(g.GuestId.ToString(), g.FirstName, g.LastName,
                            g.PhoneNumber.ToString(), g.Email);
                    }
                    else
                    {
                        table.AddRow(g.GuestId.ToString(), g.FirstName, g.LastName,
                              g.PhoneNumber.ToString(), g.Email);
                    }
                }

                AnsiConsole.Write(table);
                return true;
            }
        }
    }

    public static bool DisplayAllActiveGuestsTable
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

            table.AddColumn("[Pink1]KundId[/]");
            table.AddColumn("[Pink1]Förnamn[/]");
            table.AddColumn("[Pink1]Efternamn[/]");
            table.AddColumn("[Pink1]Telefon[/]");
            table.AddColumn("[Pink1]Email[/]");
            table.AddColumn("[Pink1]Antal aktiva bokningar[/]");

            if (dbContext.Guests.Where(g => g.IsActive == true).Count() == 0)
            {
                Console.WriteLine("Det finns inga aktiva gäster.");
                return false;
            }
            else
            {
                foreach (Guest g in dbContext.Guests.Include(a => a.ActiveBookings).Where(g => g.IsActive == true))
                {
                    if (g.ActiveBookings == null)
                    {
                        table.AddRow(g.GuestId.ToString(), g.FirstName, g.LastName,
                                    g.PhoneNumber.ToString(), g.Email, "0");
                    }
                    else
                    {
                        table.AddRow(g.GuestId.ToString(), g.FirstName, g.LastName,
                              g.PhoneNumber.ToString(), g.Email, g.ActiveBookings.Count.ToString());
                    }
                }

                AnsiConsole.Write(table);
                return true;
            }
        }
    }
}
