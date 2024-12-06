using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.TablesFolder
{
    public class CustomerTableClass
    {
        public static void CustomerTable()
        {
            var table = new Table();
            {
                table.Border = TableBorder.DoubleEdge;
                table.Centered();
            };

            table.AddColumn("[Pink1]KundId[/]");
            table.AddColumn("[Pink1]Förnamn[/]");
            table.AddColumn("[Pink1]Efternamn[/]");
            table.AddColumn("[Pink1]Personnummer[/]");
            table.AddColumn("[Pink1]Telefon[/]");
            table.AddColumn("[Pink1]Email[/]");
            table.AddColumn("[Pink1]Gatuadress[/]");
            table.AddColumn("[Pink1]Postnummer[/]");
            table.AddColumn("[Pink1]Stad[/]");
            table.AddColumn("[Pink1]Land[/]");

            table.AddRow("1", "row1");
            table.AddRow("2", "Namn2");

            AnsiConsole.Write(table);
        }
    }
}
