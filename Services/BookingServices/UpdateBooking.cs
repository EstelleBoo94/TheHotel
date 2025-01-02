using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;
using TheHotel.Services.RoomServices;

namespace TheHotel.Services.BookingServices;

public class UpdateBooking
{
    public void Update(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = BookingTableClass.DisplayAllBookingsTable(options, "ÄNDRA BOKNING");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int findId = 0;
            var findBooking = new Booking();

            while (!isValidInput)
            {
                Console.WriteLine("Ange bokningsId för bokningen du vill uppdatera:" +
                    "\nAnge 0 eller lämna tomt för att gå tillbaka");

                int.TryParse(Console.ReadLine(), out findId);
                if (findId == 0)
                {
                    return;
                }
                else if (findId < 0)
                {
                    Console.WriteLine("Ogiltigt bokningsId.");
                }
                else
                {
                    findBooking = dbContext.Bookings.Include(rb => rb.RoomsBooked).FirstOrDefault(b => b.BookingId == findId);
                    if (findBooking == null)
                    {
                        Console.WriteLine("Bokningen hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            if (findBooking != null)
            {
                var roomTypes = new List<RoomType>();
                var numberOfGuests = findBooking.NumberOfGuests;
                var numberOfRooms = findBooking.RoomsBooked.Count;
                var bookingStart = findBooking.StartDate;
                var bookingEnd = findBooking.EndDate;
                var extraBeds = findBooking.ExtraBeds;
                var findRoom = new Room();
                var bookedRooms = findBooking.RoomsBooked;
                int numberOfNights = 0;
                DateTime currentDateTime = DateTime.Now;
                DateOnly currentDate = DateOnly.FromDateTime(currentDateTime);

                var contentList = new List<string>
                {
                    findBooking.GuestId.ToString(), findBooking.NumberOfGuests.ToString(),
                    numberOfRooms.ToString(), findBooking.StartDate.ToString(),
                    findBooking.EndDate.ToString(), findBooking.ExtraBeds.ToString()
                };

                List<string> menuOptions = new List<string>
                {
                     "Antal gäster",
                      "Antal rum",
                      "Rumstyper",
                      "Startdatum",
                      "Slutdatum",
                      "Välj rum",
                      "Spara bokning"
                };

                MenuTemplate.ShowMenuWithTableForBooking("Tillbaka", "ÄNDRA BOKNING",
                    menuOptions, contentList, bookedRooms, selection =>
                {
                    switch (selection)
                    {
                        case 0:
                            Console.WriteLine("Ange med en siffra hur många gäster bokningen gäller för:");
                            int.TryParse(Console.ReadLine(), out numberOfGuests);
                            if (numberOfGuests <= 0)
                            {
                                Console.WriteLine("Ogiltigt antal gäster.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[1] = numberOfGuests.ToString();
                            }
                            break;

                        case 1:
                            if (numberOfGuests == 0)
                            {
                                Console.WriteLine("Ange antal gäster först.");
                                Console.ReadKey();
                            }
                            else if (numberOfGuests == 1)
                            {
                                Console.WriteLine("Ett rum bokas.");
                                numberOfRooms = 1;
                                contentList[2] = "1";
                                Console.ReadKey();
                            }
                            else if (numberOfGuests > 1 && numberOfGuests < 5)
                            {
                                var moreThenOneRoom = YesNoMenu.ShowYesNoMenu(
                                    "Ska fler än ett rum bokas?", "NY BOKNING");
                                if (moreThenOneRoom == 1)
                                {
                                    Console.WriteLine("Ange med en siffra hur många rum som ska bokas:");
                                    int.TryParse(Console.ReadLine(), out numberOfRooms);
                                    if (numberOfRooms <= 0)
                                    {
                                        Console.WriteLine("Ogiltigt antal rum.");
                                        Console.ReadKey();
                                        return true;
                                    }
                                    else
                                    {
                                        contentList[2] = numberOfRooms.ToString();
                                    }
                                }
                                else if (moreThenOneRoom == 2)
                                {
                                    numberOfRooms = 1;
                                    contentList[2] = "1";
                                }
                            }
                            else if (numberOfGuests >= 5)
                            {
                                Console.WriteLine("Ange med en siffra hur många rum som ska bokas:");
                                int.TryParse(Console.ReadLine(), out numberOfRooms);
                                if (numberOfRooms <= 0)
                                {
                                    Console.WriteLine("Ogiltigt antal rum.");
                                    Console.ReadKey();
                                    return true;
                                }
                                else
                                {
                                    contentList[2] = numberOfRooms.ToString();
                                }
                            }
                            break;

                        case 2:
                            if (numberOfRooms == 0)
                            {
                                Console.WriteLine($"Ange antal rum först.");
                                Console.ReadKey();
                            }
                            else
                            {
                                roomTypes.Clear();
                                for (int i = 1; i <= numberOfRooms; i++)
                                {

                                    var selectedRoomType = RoomTypeMenu.ShowRoomTypeMenu(
                                        $"Välj rumstyp för rum {i}:", "NY BOKNING");
                                    if (selectedRoomType == 1)
                                    {
                                        roomTypes.Add(RoomType.Single);
                                        Console.WriteLine("Enkelrum sparat.");
                                        Console.ReadKey();
                                    }
                                    else if (selectedRoomType == 2)
                                    {
                                        roomTypes.Add(RoomType.Double);
                                        Console.WriteLine("Dubbelrum sparat.");
                                        Console.ReadKey();
                                    }
                                    else if (selectedRoomType == 3)
                                    {
                                        roomTypes.Add(RoomType.Suite);
                                        Console.WriteLine("Svit sparad.");
                                        Console.ReadKey();
                                    }

                                }

                            }
                            break;

                        case 3:
                            if (roomTypes.Count == 0)
                            {
                                Console.WriteLine("Ange rumstyper först.");
                                Console.ReadKey();
                            }
                            else
                            {
                                bookedRooms.Clear();
                                bookingStart = CalenderMenu.ShowCalenderMenu(
                                    "Välj startdatum för bokningen:", "ÄNDRA BOKNING", options, roomTypes);
                                contentList[3] = bookingStart.ToString();
                            }
                            break;

                        case 4:
                            if (bookingStart == DateOnly.MinValue)
                            {
                                Console.WriteLine("Ange startdatum först.");
                                Console.ReadKey();
                            }
                            else
                            {
                                bookedRooms.Clear();
                                Console.WriteLine("Ange med en siffra hur många nätter gästen vill boka:");
                                int.TryParse(Console.ReadLine(), out numberOfNights);
                                if (numberOfNights <= 0)
                                {
                                    Console.WriteLine("Ogiltigt antal nätter.");
                                    Console.ReadKey();
                                    return true;
                                }
                                else
                                {
                                    bookingEnd = bookingStart.AddDays(numberOfNights);
                                    contentList[4] = bookingEnd.ToString();
                                }
                            }
                            break;

                        case 5:
                            CheckIfAvailiable ifAvailiable = new();
                            var avaliableRooms = ifAvailiable.GetAvailiableRooms(options,
                                bookingStart, bookingEnd, roomTypes);
                            if (avaliableRooms.Count == 0)
                            {
                                Console.WriteLine("Inga lediga rum för det valda datumintervallet." +
                                    " Välj nya datum.");
                                Console.ReadKey();
                            }
                            else if (avaliableRooms.Count < numberOfRooms)
                            {
                                Console.WriteLine("För få lediga rum för det valda datumintervallet." +
                                    " Välj nya datum.");
                                Console.ReadKey();
                            }
                            else
                            {

                                for (int i = 1; i <= numberOfRooms; i++)
                                {
                                    RoomTableClass.DisplayRoomForBooking(
                                        avaliableRooms, "Lediga rum för perioden:", "NY BOKNING");

                                    int findId = 0;
                                    Console.WriteLine($"\nAnge rumsId för bokning av rum nummer {i}:");
                                    int.TryParse(Console.ReadLine(), out findId);

                                    if (findId <= 0)
                                    {
                                        Console.WriteLine("Ogiltigt rumsId.");
                                    }
                                    else
                                    {
                                        findRoom = avaliableRooms.FirstOrDefault(r => r.RoomId == findId);
                                        if (findRoom == null)
                                        {
                                            Console.WriteLine("Rummet hittades inte.");
                                        }
                                        else
                                        {
                                            avaliableRooms.Remove(findRoom);
                                            bookedRooms.Add(findRoom);
                                        }
                                    }

                                    if (findRoom.Size > 24 && numberOfGuests > 1)
                                    {
                                        var extraBed = YesNoMenu.ShowYesNoMenu(
                                            "Vill du lägga till en extrasäng i rummet?", "NY BOKNING");
                                        if (extraBed == 1)
                                        {
                                            Console.WriteLine("Extrasäng tillagd.");
                                            Console.ReadKey();
                                            extraBeds += 1;
                                            contentList[5] = extraBeds.ToString();
                                        }

                                        if (findRoom.Size > 30 && numberOfGuests > 1)
                                        {
                                            extraBed = YesNoMenu.ShowYesNoMenu(
                                                "Vill du lägga till en till extrasäng i rummet?", "NY BOKNING");
                                            if (extraBed == 1)
                                            {
                                                Console.WriteLine("Extrasäng tillagd.");
                                                Console.ReadKey();
                                                extraBeds += 1;
                                                contentList[5] = extraBeds.ToString();
                                            }
                                        }

                                        Console.WriteLine("Rummet är bokat.");
                                    }

                                }
                            }
                            break;

                        case 6:
                            if (numberOfGuests == 0 || bookingStart == DateOnly.MinValue
                        || bookingEnd == DateOnly.MinValue || bookedRooms.Count == 0)
                            {
                                Console.WriteLine("Fyll i tomma fält.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                foreach (var room in bookedRooms)
                                {
                                    dbContext.Attach(room);
                                }

                                findBooking.StartDate = bookingStart;
                                findBooking.EndDate = bookingEnd;
                                findBooking.NumberOfGuests = numberOfGuests;
                                findBooking.RoomsBooked = bookedRooms;
                                findBooking.ExtraBeds = extraBeds;

                                dbContext.SaveChanges();

                                var findInvoice = dbContext.Invoices.First(i => i.BookingId == findBooking.BookingId);
                                findInvoice.Amount = bookedRooms.Sum(r => r.Price);
                                findInvoice.DueDate = currentDate.AddDays(10);
                                dbContext.SaveChanges();

                                Console.WriteLine("Bokningen och tillhörande faktura är uppdaterade.\nTryck valfri tangent för att fortsätta.");
                                Console.ReadKey();
                                return false;
                            }
                    }
                    return true;
                });

            }

        }
    }
}
