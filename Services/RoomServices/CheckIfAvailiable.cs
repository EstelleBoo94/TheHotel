using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Models;

namespace TheHotel.Services.RoomServices;

public class CheckIfAvailiable
{
    public List<Room> GetAvailiableRooms(DbContextOptions<ApplicationDbContext> options,
    DateOnly startDate, DateOnly endDate, List<RoomType> selectedRoomTypes)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            var activeBookings = dbContext.Bookings
                .Where(b => b.StartDate < endDate && b.EndDate > startDate)
                .Include(b => b.RoomsBooked)
                .ToList();

            var allRooms = dbContext.Rooms
                .Where(r => selectedRoomTypes.Contains(r.Type) && r.IsActive == true)
                .ToList();

            var unavailableRoomIds = activeBookings
                .SelectMany(b => b.RoomsBooked)
                .Select(r => r.RoomId)
                .Distinct()
                .ToList();

            var availableRooms = allRooms
                .Where(r => !unavailableRoomIds.Contains(r.RoomId))
                .ToList();

            return availableRooms;
        }
    }

}
