using Spectre.Console;

namespace TheHotel.Displays.CalenderFolder;

public static class Calender
{
    public static void RenderCalendar(DateOnly selectedDate)
    {

        var testBookingStart = new DateTime(2024, 12, 24);
        var testBookingEnd = new DateTime(2024, 12, 26);
        var calendarContent = new StringWriter();

        AnsiConsole.MarkupLine("Använd piltangenter för att \n" +
            "navigera och Enter för att välja.");

        calendarContent.WriteLine($"            [hotpink]{selectedDate:MMMM}[/]".ToUpper());
        calendarContent.WriteLine("[orchid1]Mån  Tis  Ons  Tor  Fre  Lör  Sön[/]");
        calendarContent.WriteLine("[orchid1]─────────────────────────────────[/]");

        DateTime firstDayOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
        int startDay = (int)firstDayOfMonth.DayOfWeek;
        startDay = (startDay == 0) ? 6 : startDay - 1;

        for (int i = 0; i < startDay; i++)
        {
            calendarContent.Write("     ");
        }

        for (int day = 1; day <= daysInMonth; day++)
        {
            if (day == selectedDate.Day)
            {
                calendarContent.Write($"[royalblue1]{day,2}[/]   ");
            }
            else
            {
                calendarContent.Write($"{day,2}   ");
            }

            if ((startDay + day) % 7 == 0)
            {
                calendarContent.WriteLine();
            }
        }

        var panel = new Panel(calendarContent.ToString())
        {
            Border = BoxBorder.Double,
            Header = new PanelHeader(($"[hotpink]{selectedDate:yyyy}[/]"), Justify.Center)
        };

        var paddedPanel = new Padder(panel).Padding(3, 2);

        AnsiConsole.Write(paddedPanel);

    }
}
