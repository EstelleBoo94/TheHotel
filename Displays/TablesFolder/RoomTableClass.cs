using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Displays.TablesFolder
{
    public class RoomTableClass
    {
        public static void DisplayRoomTable(List<string> content)
        {

            var table = new Table();
            {
                table.Border = TableBorder.DoubleEdge;
                table.Centered();
            };

            table.AddColumn("[Pink1]Rumsnummer[/]");
            table.AddColumn("[Pink1]Rumstyp[/]");
            table.AddColumn("[Pink1]Storlek[/]");
            table.AddColumn("[Pink1]Ledig[/]");

            table.AddRow(content[0], content[1], content[2], content[3]);

            AnsiConsole.Write(table);
        }
    }
}
