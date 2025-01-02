using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using TheHotel.Data;
using TheHotel.Models;

namespace TheHotel.Displays.TablesFolder;

public class RoomTableClass
{
    public static void DisplayRoomTable(List<string> content)
    {

        var table = new Table();
        {
            table.Border = TableBorder.DoubleEdge;
            table.BorderStyle = Style.Parse("pink1");
        };

        table.AddColumn("[Pink1]Rumsnummer[/]");
        table.AddColumn("[Pink1]Rumstyp[/]");
        table.AddColumn("[Pink1]Storlek[/]");
        table.AddColumn("[Pink1]Pris[/]");
        table.AddColumn("[Pink1]Status[/]");

        table.AddRow(content[0], content[1], content[2], content[3], content[4]);

        AnsiConsole.Write(table);
    }

    public static void DisplayRoomTableWithoutActiveTab(List<string> content)
    {

        var table = new Table();
        {
            table.Border = TableBorder.DoubleEdge;
            table.BorderStyle = Style.Parse("pink1");
        };

        table.AddColumn("[Pink1]Rumsnummer[/]");
        table.AddColumn("[Pink1]Rumstyp[/]");
        table.AddColumn("[Pink1]Storlek[/]");
        table.AddColumn("[Pink1]Pris[/]");

        table.AddRow(content[0], content[1], content[2], content[3]);

        AnsiConsole.Write(table);
    }

    public static bool DisplayAllRoomsTable
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

            table.AddColumn("[Pink1]RumsId[/]");
            table.AddColumn("[Pink1]Rumsnummer[/]");
            table.AddColumn("[Pink1]Rumstyp[/]");
            table.AddColumn("[Pink1]Storlek[/]");
            table.AddColumn("[Pink1]Pris[/]");
            table.AddColumn("[Pink1]Status[/]");

            if (dbContext.Rooms.Count() == 0)
            {
                Console.WriteLine("Det finns inga rum.");
                return false;
            }
            else
            {
                foreach (Room r in dbContext.Rooms.Where(r => r.IsActive == true))
                {
                    if (r.Type == RoomType.Single)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Enkelrum", r.Size.ToString(), r.Price.ToString(), "Aktivt");
                    }
                    else if (r.Type == RoomType.Double)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Dubbelrum", r.Size.ToString(), r.Price.ToString(), "Aktivt");
                    }
                    else if (r.Type == RoomType.Suite)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Svit", r.Size.ToString(), r.Price.ToString(), "Aktivt");
                    }
                }
                foreach (Room r in dbContext.Rooms.Where(r => r.IsActive == false))
                {
                    if (r.Type == RoomType.Single)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Enkelrum", r.Size.ToString(), r.Price.ToString(), "Avstängt");
                    }
                    else if (r.Type == RoomType.Double)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Dubbelrum", r.Size.ToString(), r.Price.ToString(), "Avstängt");
                    }
                    else if (r.Type == RoomType.Suite)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(), 
                            "Svit", r.Size.ToString(), r.Price.ToString(), "Avstängt");
                    }
                }

                AnsiConsole.Write(table);
                return true;
            }
        }
    }

    public static bool DisplayAllDeactivatedRoomsTable
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

            table.AddColumn("[Pink1]RumsId[/]");
            table.AddColumn("[Pink1]Rumsnummer[/]");
            table.AddColumn("[Pink1]Rumstyp[/]");
            table.AddColumn("[Pink1]Storlek[/]");
            table.AddColumn("[Pink1]Pris[/]");
            table.AddColumn("[Pink1]Status[/]");

            if (dbContext.Rooms.Where(r => r.IsActive == false).Count() == 0)
            {
                Console.WriteLine("Det finns inga avaktiverade rum.");
                return false;
            }
            else
            {
                foreach (Room r in dbContext.Rooms.Where(r => r.IsActive == false))
                {
                    if (r.Type == RoomType.Single)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Enkelrum", r.Size.ToString(), r.Price.ToString(), "Avstängt");
                    }
                    else if (r.Type == RoomType.Double)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(), 
                            "Dubbelrum", r.Size.ToString(), r.Price.ToString(), "Avstängt");
                    }
                    else if (r.Type == RoomType.Suite)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Svit", r.Size.ToString(), r.Price.ToString(), "Avstängt");
                    }
                }
                AnsiConsole.Write(table);
                return true;
            }
        }
    }

    public static bool DisplayAllActiveRoomsTable
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

            table.AddColumn("[Pink1]RumsId[/]");
            table.AddColumn("[Pink1]Rumsnummer[/]");
            table.AddColumn("[Pink1]Rumstyp[/]");
            table.AddColumn("[Pink1]Storlek[/]");
            table.AddColumn("[Pink1]Pris[/]");
            table.AddColumn("[Pink1]Status[/]");

            if (dbContext.Rooms.Where(r => r.IsActive == true).Count() == 0)
            {
                Console.WriteLine("Det finns inga aktiva rum.");
                return false;
            }
            else
            {
                foreach (Room r in dbContext.Rooms.Where(r => r.IsActive == true))
                {
                    if (r.Type == RoomType.Single)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Enkelrum", r.Size.ToString(), r.Price.ToString(), "Aktivt");
                    }
                    else if (r.Type == RoomType.Double)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Dubbelrum", r.Size.ToString(), r.Price.ToString(), "Aktivt");
                    }
                    else if (r.Type == RoomType.Suite)
                    {
                        table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                            "Svit", r.Size.ToString(), r.Price.ToString(), "Aktivt");
                    }
                }
                AnsiConsole.Write(table);
                return true;
            }
        }
    }

    public static void DisplayRoomForBooking(List<Room> rooms, string prompt, string headerText)
    {
        Console.Clear();
        Header.DisplayHeader($"{headerText}");
        Console.WriteLine($"{prompt}");
        var table = new Table();
        {
            table.Border = TableBorder.DoubleEdge;
            table.BorderStyle = Style.Parse("pink1");
        };

        table.AddColumn("[Pink1]RumsId[/]");
        table.AddColumn("[Pink1]Rumsnummer[/]");
        table.AddColumn("[Pink1]Rumstyp[/]");
        table.AddColumn("[Pink1]Storlek[/]");
        table.AddColumn("[Pink1]Pris[/]");

        foreach (Room r in rooms)
        {
            if (r.Type == RoomType.Single)
            {
                table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                    "Enkelrum", r.Size.ToString(), r.Price.ToString());
            }
            else if (r.Type == RoomType.Double)
            {
                table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(),
                    "Dubbelrum", r.Size.ToString(), r.Price.ToString());
            }
            else if (r.Type == RoomType.Suite)
            {
                table.AddRow(r.RoomId.ToString(), r.RoomNumber.ToString(), 
                    "Svit", r.Size.ToString(), r.Price.ToString());
            }
        }

        AnsiConsole.Write(table);
    }
}
