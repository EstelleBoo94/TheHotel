using Microsoft.EntityFrameworkCore;
using TheHotel.Models;

namespace TheHotel.Data;

public class DataInitializer
{
    public void MigrateAndSeed(ApplicationDbContext dbContext)
    {
        dbContext.Database.Migrate();
        SeedRooms(dbContext);
        SeedGuest(dbContext);
        dbContext.SaveChanges();
    }

    private void SeedGuest(ApplicationDbContext dbContext)
    {
        if (!dbContext.Guests.Any(g => g.GuestId == 1))
        {
            dbContext.Guests.Add(new Guest
            {
                FirstName = "Estelle",
                LastName = "Boo",
                SSN = 19940618,
                PhoneNumber = "0738321224",
                Email = "estelle@gmail.com",
                StreetAddress = "Midälvavägen",
                PostalCode = 85353,
                City = "Sundsvall",
                Country = "Sverige",
                IsActive = true,
            });
        }
        if (!dbContext.Guests.Any(g => g.GuestId == 2))
        {
            dbContext.Guests.Add(new Guest
            {
                FirstName = "Leia",
                LastName = "Schäfer",
                SSN = 20000710,
                PhoneNumber = "0731231234",
                Email = "leia@gmail.com",
                StreetAddress = "Midälvavägen",
                PostalCode = 85353,
                City = "Sundsvall",
                Country = "Sverige",
                IsActive = true,
            });
        }
        if (!dbContext.Guests.Any(g => g.GuestId == 3))
        {
            dbContext.Guests.Add(new Guest
            {
                FirstName = "Jasmin",
                LastName = "Boo",
                SSN = 19920326,
                PhoneNumber = "0733213210",
                Email = "jasmin@gmail.com",
                StreetAddress = "Uppsalavägen",
                PostalCode = 85123,
                City = "Sundsvall",
                Country = "Sverige",
                IsActive = true,
            });
        }
        if (!dbContext.Guests.Any(g => g.GuestId == 4))
        {
            dbContext.Guests.Add(new Guest
            {
                FirstName = "Jul",
                LastName = "Tomten",
                SSN = 19000101,
                PhoneNumber = "0711111111",
                Email = "santa@gmail.com",
                StreetAddress = "Snövägen",
                PostalCode = 11111,
                City = "Tomtestaden",
                Country = "Nordpolen",
                IsActive = true,
            });
        }
    }

    private void SeedRooms(ApplicationDbContext dbContext)
    {
        if (!dbContext.Rooms.Any(r => r.RoomNumber == 100))
        {
            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 100,
                Type = RoomType.Single,
                Size = 10,
                Price = 1000,
                IsActive = true,
            });
        }
        if (!dbContext.Rooms.Any(r => r.RoomNumber == 101))
        {
            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 101,
                Type = RoomType.Single,
                Size = 10,
                Price = 1000,
                IsActive = true,
            });
        }
        if (!dbContext.Rooms.Any(r => r.RoomNumber == 102))
        {
            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 102,
                Type = RoomType.Double,
                Size = 23,
                Price = 2000,
                IsActive = true,
            });
        }
        if (!dbContext.Rooms.Any(r => r.RoomNumber == 103))
        {
            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 103,
                Type = RoomType.Double,
                Size = 26,
                Price = 2000,
                IsActive = true,
            });
        }
        if (!dbContext.Rooms.Any(r => r.RoomNumber == 104))
        {
            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 104,
                Type = RoomType.Suite,
                Size = 35,
                Price = 3000,
                IsActive = true,
            });
        }

    }
}
