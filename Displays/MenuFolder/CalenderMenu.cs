using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.CalenderFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;
using TheHotel.Services.RoomServices;

namespace TheHotel.Displays.MenuFolder;

public static class CalenderMenu
{
    public static DateOnly ShowCalenderMenu(string prompt, string headerText,
        DbContextOptions<ApplicationDbContext> options,
        List<RoomType> selectedRoomTypes)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            CheckIfAvailiable checkIfAvailiable = new();

            var testBookingStart = new DateOnly(2024, 12, 24);
            var testBookingEnd = new DateOnly(2024, 12, 26);
            DateTime currentDateTime = DateTime.Now;
            DateOnly currentDate = DateOnly.FromDateTime(currentDateTime);
            DateOnly selectedDate = new DateOnly(currentDate.Year, currentDate.Month, 1);
            DateOnly startDate = DateOnly.MinValue;

            while (true)
            {
                Console.Clear();
                Header.DisplayHeader($"{headerText}");
                Console.WriteLine($"{prompt}");
                Calender.RenderCalendar(selectedDate);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        selectedDate = selectedDate.AddDays(1);
                        break;
                    case ConsoleKey.LeftArrow:
                        selectedDate = selectedDate.AddDays(-1);
                        break;
                    case ConsoleKey.UpArrow:
                        selectedDate = selectedDate.AddDays(-7);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedDate = selectedDate.AddDays(7);
                        break;
                    case ConsoleKey.Enter:
                        if (selectedDate < currentDate)
                        {
                            Console.WriteLine("Du kan inte boka ett rum för ett datum som redan varit.");
                            Console.WriteLine("Tryck för att fortsätta");
                            Console.ReadKey();
                            continue;
                        }
                        
                        Console.WriteLine("Tryck för att fortsätta");
                        Console.ReadKey();
                        return selectedDate;

                }
            }
        }
    }

}
