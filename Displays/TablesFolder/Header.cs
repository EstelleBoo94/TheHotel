using Spectre.Console;

namespace TheHotel.Displays.TablesFolder;

public static class Header
{
    public static void DisplayHeader(string headerText)
    {
        var table = new Table();
        {
            table.Border = TableBorder.HeavyHead;
            table.BorderStyle = Style.Parse("pink1");
            table.Expand();
        };

        table.AddColumn($" [Pink1 bold]{headerText}[/]");

        AnsiConsole.Write(table);
    }
}
