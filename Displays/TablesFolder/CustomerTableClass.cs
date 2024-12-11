using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Models;
using TheHotel.Services;

namespace TheHotel.Displays.TablesFolder
{
    public class CustomerTableClass
    {
        public static void DisplayCustomerTable(List<string> content)
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

            table.AddRow(content[0], content[1], content[2], content[3],
                content[4], content[5], content[6], content[7], content[8], content[9]);

            AnsiConsole.Write(table);
        }

        public static void DisplayAllCustomersTable()
        {
            CustomerList customers = new CustomerList();
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

            foreach (Customer c in customers.Customers)
            {
                table.AddRow(c.CustomerId.ToString(), c.FirstName, c.LastName, c.SSN.ToString(),
                                c.PhoneNumber.ToString(), c.Email, c.StreetAddress, c.PostalCode.ToString(), c.City, c.Country);
            }

            AnsiConsole.Write(table);
        }
    }
}
